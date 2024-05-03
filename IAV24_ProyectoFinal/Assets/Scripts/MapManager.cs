using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThreadNinja;

public class MapManager : MonoBehaviour {
	public InfluenceMapControl[] influenceMaps;
	public DerivedMapControl[] derivedMaps;
	public float updateDelay=.2f;
	float timer;
	int Width, Height;

	[SerializeField]
	bool threaded=true;
	void Update()
	{
		Width = influenceMaps [0]._influenceMap.Width;
		Height = influenceMaps [0]._influenceMap.Height;
		if (Time.time > timer) {
			timer = Time.time + updateDelay;
			if (threaded)
				this.StartCoroutineAsync( PropagateAsync());
			else
				StartCoroutine( Propagate());
		}
	}

	public int granularity = 10;
	IEnumerator PropagateAsync()
	{
		yield return Ninja.JumpToUnity;
		for (int i=0; i<influenceMaps.Length; i++)
			influenceMaps[i]._influenceMap.UpdatePropagatorsAndBounds ();
		yield return Ninja.JumpBack;
		for (int i = 0; i < influenceMaps [0]._influenceMap.Width; i += granularity) {
			UpdatePropagation (i, Mathf.Min (i + granularity, influenceMaps [0]._influenceMap.Width));
		}
		yield return null;
		for (int i=0; i<influenceMaps.Length; i++)
			influenceMaps[i]._influenceMap.UpdateInfluenceBuffer ();
		yield return null;
		for (int i=0; i<derivedMaps.Length; i++)
			derivedMaps[i]._influenceMap.UpdateInfluenceBuffer ();
	}

	IEnumerator Propagate()
	{
		for (int i=0; i<influenceMaps.Length; i++)
			influenceMaps[i]._influenceMap.UpdatePropagatorsAndBounds ();
		for (int i = 0; i < influenceMaps [0]._influenceMap.Width; i += granularity) {
			UpdatePropagation (i, Mathf.Min (i + granularity, influenceMaps [0]._influenceMap.Width));
			yield return null;
		}
		for (int i=0; i<influenceMaps.Length; i++)
			influenceMaps[i]._influenceMap.UpdateInfluenceBuffer ();
		for (int i=0; i<derivedMaps.Length; i++)
			derivedMaps[i]._influenceMap.UpdateInfluenceBuffer ();
		yield return null;
	}

	Vector2I[] retVal = new Vector2I[8];

	void UpdatePropagation(int start, int end)
	{
//		for (int x = 0; x < influenceMaps[0]._influenceMap.Width; ++x)
		for (int x = start; x < end; ++x)
		{
			for (int y = 0; y < influenceMaps[0]._influenceMap.Height; ++y)
			{
				//influence maps
				GetNeighbors(ref retVal, x, y);
				for (int m=0; m<influenceMaps.Length;m++){
					float maxInf = 0.0f;
					float minInf = 0.0f;
					for (int i=0;i<8;i++)
					{
						Vector2I n = retVal [i];
						if (n.d!=0) {
								float inf = influenceMaps[m]._influenceMap._influencesBuffer [n.x +Width* n.y] * Mathf.Exp (-influenceMaps[m]._influenceMap.Decay * n.d); //* Decay;
							maxInf = Mathf.Max (inf, maxInf);
							minInf = Mathf.Min (inf, minInf);
						}
					}

					//momentum and decay
					int j = x + Width * y;
					if (Mathf.Abs(minInf) > maxInf)
					{
							influenceMaps[m]._influenceMap._influences[j] = Mathf.Lerp(influenceMaps[m]._influenceMap._influencesBuffer[j], minInf, influenceMaps[m]._influenceMap.Momentum);
					}
					else
					{
							influenceMaps[m]._influenceMap._influences[j] = Mathf.Lerp(influenceMaps[m]._influenceMap._influencesBuffer[j], maxInf, influenceMaps[m]._influenceMap.Momentum);
						}
				}

				// derived maps
				for (int d = 0; d < derivedMaps.Length; d++) {
					int i = x + influenceMaps [0]._influenceMap.Width * y;
					derivedMaps[d]._influenceMap._influences[i]= derivedMaps[d].weightMap1 * (derivedMaps[d].abs1 ? 
					                                                                          Mathf.Abs (derivedMaps[d].map1._influenceMap._influences [i]) 
					                                                                          : derivedMaps[d].map1._influenceMap._influences [i])
						+ derivedMaps[d].weightMap2 * (derivedMaps[d].abs2 ? 
						                               Mathf.Abs (derivedMaps[d].map2._influenceMap._influences[i]) 
						                               : derivedMaps[d].map2._influenceMap._influences [i]);
				}
			}
//			if (x % 10==0)
//				yield return null;
		}
	}

	void GetNeighbors(ref Vector2I[] array, int x, int y)
	{
		InfluenceMap.InitVector2IArray(ref retVal);
		if (x > 0) {
			retVal [0] = new Vector2I (x - 1, y);
		}
		if (x < Width - 1) {
			retVal [1] = new Vector2I (x + 1, y);
		}
		if (y > 0) {
			retVal [2] = new Vector2I (x, y - 1);
		}
		if (y < Height - 1) {
			retVal [3] = new Vector2I (x, y + 1);
		}

		// diagonals

		if (x > 0 && y > 0)
		{
			retVal[4] = new Vector2I(x-1, y-1, 1.4142f);
		}
		if (x < Width-1 && y < Height-1)
		{
			retVal[5] = new Vector2I(x+1, y+1, 1.4142f);
		}
		if (x > 0 && y < Height-1)
		{
			retVal[6] = new Vector2I(x-1, y+1, 1.4142f);
		}
		if (x < Width-1 && y > 0)
		{
			retVal[7] = new Vector2I(x+1, y-1, 1.4142f);
		}

		// zero out if they're inside an obstacle
		for (int i=0; i<8;i++){
			if (influenceMaps[0]._influenceMap.IsInsideObstacle (retVal [i]))
				retVal [i] = new Vector2I (0, 0, 0);
		} 
	}
}
