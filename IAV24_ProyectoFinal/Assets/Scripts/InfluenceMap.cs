/*
Copyright (C) 2012 Anomalous Underdog

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ThreadNinja;

[System.Serializable]
public class InfluenceMap : IGridData
{
	List<IPropagator> _propagators = new List<IPropagator>();
	List<InfluenceObstacle> _obstacles = new List<InfluenceObstacle>();
	BoundsI[] _obstaclesBounds = new BoundsI[1000];

	internal float[] _influences;
	internal float[] _influencesBuffer;
	public float Decay { get; set; }
	public float Momentum { get; set; }
	public int Width { get{ return width; } }
	public int Height { get{ return height; } }
	public float GetValue(int x, int y)
	{
		return _influences[x + Width* y];
	}

	int width, height;
	public InfluenceMap(int size, float decay, float momentum)
	{
		width = height = size;
		_influences = new float[size* size];
		_influencesBuffer = new float[size* size];
		Decay = decay;
		Momentum = momentum;
	}
	
	public InfluenceMap(int width, int height, float decay, float momentum)
	{
		this.width = width;
		this.height = height;
		_influences = new float[width* height];
		_influencesBuffer = new float[width* height];
		Decay = decay;
		Momentum = momentum;
	}

	public float GetInfluence(Vector2I pos)
	{
		if (pos.x < Width && pos.y < Height && pos.x >= 0 && pos.y >= 0)
			return _influencesBuffer [pos.x + Width* pos.y];
		else
			return -Mathf.Infinity; //completely arbitrary way of handling walls :(
	}

	public void SetInfluence(Vector2I pos, float value)
	{
		if (pos.x < Width && pos.y < Height && pos.x >=0 &&pos.y>=0)
		{
			_influencesBuffer[pos.x + Width* pos.y] = _influences[pos.x + Width* pos.y] = value;;
		}
	}

	public void CopyInfluences(float[] inf)
	{
		inf.CopyTo (_influences, 0);
		UpdateInfluenceBuffer ();
	}

	public void RegisterPropagator(IPropagator p)
	{
		_propagators.Add(p);
	}
	public void DeregisterPropagator(IPropagator p)
	{
		_propagators.Remove(p);
	}
	InfluenceMapControl mapControl;
	public void RegisterObstacle(InfluenceObstacle o, InfluenceMapControl _mapControl)
	{
		_obstacles.Add(o);
		mapControl = _mapControl;
	}
	public void DeregisterObstacle(InfluenceObstacle o, InfluenceMapControl _mapControl)
	{
		_obstacles.Remove(o);
		mapControl = _mapControl;
	}

	public IEnumerator PropagateAsync()
	{
//		yield return Ninja.JumpToUnity;
//		UpdatePropagatorsAndBounds ();
		yield return Ninja.JumpBack;
//		UpdatePropagation ();
//		UpdateInfluenceBuffer ();
	}

	public void Propagate()
	{
//		UpdatePropagatorsAndBounds ();
//		UpdatePropagation ();
//		UpdateInfluenceBuffer ();
	}
				
	internal void UpdatePropagatorsAndBounds()
	{
		foreach (IPropagator p in _propagators)
		{
			SetInfluence(p.GridPosition, p.Value);
		}
		for (int i=0; i<_obstacles.Count ; i++)
			_obstaclesBounds[i] = new BoundsI(_obstacles[i].GetBounds (), mapControl );	
	}

	void UpdatePropagation()
	{
		for (int x = 0; x < Width; ++x)
		{
			for (int y = 0; y < Height; ++y)
			{
				float maxInf = 0.0f;
				float minInf = 0.0f;
				GetNeighbors(ref retVal, x, y);
				for (int i=0;i<8;i++)
				{
					Vector2I n = retVal [i];
					if (n.d!=0) {
						float inf = _influencesBuffer [n.x + Width* n.y] * Mathf.Exp (-Decay * n.d); //* Decay;
						maxInf = Mathf.Max (inf, maxInf);
						minInf = Mathf.Min (inf, minInf);
					}
				}
				
				if (Mathf.Abs(minInf) > maxInf)
				{
					_influences[x+ Width* y] = Mathf.Lerp(_influencesBuffer[x+ Width* y], minInf, Momentum);
				}
				else
				{
					_influences[x+ Width* y] = Mathf.Lerp(_influencesBuffer[x+ Width* y], maxInf, Momentum);
				}
			}
		}
	}

	internal void UpdateInfluenceBuffer()
	{
		_influences.CopyTo (_influencesBuffer, 0);
	}

	Vector2I[] retVal = new Vector2I[8];
	public static void InitVector2IArray(ref Vector2I[] array)
	{
		for (int i = 0; i < array.Length; i++)
			array [i] = new Vector2I (0, 0, 0);
	}

	void GetNeighbors(ref Vector2I[] array, int x, int y)
	{
		InitVector2IArray(ref retVal);
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
			if (IsInsideObstacle (retVal [i]))
				retVal [i] = new Vector2I (0, 0, 0);
		} 
	}

    internal bool IsInsideObstacle (Vector2I pos){
		for (int i=0;i<_obstacles.Count; i++)
			if (_obstaclesBounds[i].Contains (pos))
				return true;
		return false;
	}
}

[System.Serializable]
public struct Vector2I
{
	public int x;
	public int y;
	public float d;

	public Vector2I(int nx, int ny)
	{
		x = nx;
		y = ny;
		d = 1;
	}

	public Vector2I(int nx, int ny, float nd)
	{
		x = nx;
		y = ny;
		d = nd;
	}

	public static Vector2I forward{get{return new Vector2I(0,1);  }}
	public static Vector2I right{get{return new Vector2I(1,0);  }}

	public static Vector2I operator +(Vector2I v1, Vector2I v2){return new Vector2I(v1.x+v2.x, v1.y+v2.y);}
	public static Vector2I operator -(Vector2I v1, Vector2I v2){return new Vector2I(v1.x-v2.x, v1.y-v2.y);}

}

