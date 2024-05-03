/*
Copyright (C) 2012 Anomalous Underdog

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using UnityEngine;
using System.Collections;
using ThreadNinja;

public abstract class MapServer:MonoBehaviour
{
	internal InfluenceMap _influenceMap;
	public virtual Vector2I GetGridPosition (Vector3 pos){return new Vector2I();}
	public float GetInfluence(Vector2I position)
	{
		return _influenceMap.GetInfluence(position);
	}
	public float GetInfluence(int x, int y)
	{
		return _influenceMap.GetInfluence(new Vector2I( x,y));
	}
	public virtual Transform GetUpperRight(){return null;}
	public virtual Transform GetBottomLeft(){return null;}
	public virtual float GetGridSize(){return 0;}
	public Vector2I GetGridLentgh ()
	{
		return new Vector2I( _influenceMap.Width, _influenceMap.Height);
	}
}

[DefaultExecutionOrder(-1000)]
public class InfluenceMapControl : MapServer
{
	[SerializeField]
	GridDisplay _display;

	[SerializeField]
	Transform _bottomLeft;
	
	[SerializeField]
	Transform _upperRight;
	
	[SerializeField]
	float _gridSize;
	
	[SerializeField]
	float _decay = 0.3f;
	
	[SerializeField]
	float _momentum = 0.8f;

	void CreateMap()
	{
		// how many of gridsize is in Mathf.Abs(_upperRight.positon.x - _bottomLeft.position.x)
		int width = (int)(Mathf.Abs(_upperRight.position.x - _bottomLeft.position.x) / _gridSize);
		int height = (int)(Mathf.Abs(_upperRight.position.z - _bottomLeft.position.z) / _gridSize);
		
		_influenceMap = new InfluenceMap(width, height, _decay, _momentum);
		
		_display.SetGridData(_influenceMap);
		_display.CreateMesh(_bottomLeft.position, _gridSize);
	}

	public void RegisterPropagator(IPropagator p)
	{
		_influenceMap.RegisterPropagator(p);
	}
	public void DeregisterPropagator(IPropagator p)
	{
		_influenceMap.DeregisterPropagator(p);
	}

	public void RegisterObstacle(InfluenceObstacle o)
	{
		_influenceMap.RegisterObstacle(o, this);
	}
	public void DeregisterObstacle(InfluenceObstacle o)
	{
		_influenceMap.DeregisterObstacle(o,this);
	}

	public override Vector2I GetGridPosition(Vector3 pos)
	{
		int x = (int)((pos.x - _bottomLeft.position.x)/_gridSize);
		int y = (int)((pos.z - _bottomLeft.position.z)/_gridSize);

		return new Vector2I(x, y);
	}

	public void GetMovementLimits(out Vector3 bottomLeft, out Vector3 topRight)
	{
		bottomLeft = _bottomLeft.position;
		topRight = _upperRight.position;
	}

	public override Transform GetUpperRight ()
	{
		return _upperRight;
	}

	public override Transform GetBottomLeft ()
	{
		return _bottomLeft;
	}

	public override float GetGridSize ()
	{
		return _gridSize;
	}

	//to stagger maps
	void Awake()
	{
		CreateMap();	
	}

	void SetInfluence(Vector2I pos, float value)
	{
		_influenceMap.SetInfluence(pos, value);
	}

	void Update()
	{
		_influenceMap.Decay = _decay;
		_influenceMap.Momentum = _momentum;
		
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit mouseHit;
		if (Physics.Raycast(mouseRay, out mouseHit) && Input.GetMouseButton(0) || Input.GetMouseButton(1))
		{
			// is it within the grid
			// if so, call SetInfluence in that grid position to 1.0
			Vector3 hit = mouseHit.point;
			if (hit.x > _bottomLeft.position.x && hit.x < _upperRight.position.x && hit.z > _bottomLeft.position.z && hit.z < _upperRight.position.z)
			{
				Vector2I gridPos = GetGridPosition(hit);

				if (gridPos.x < _influenceMap.Width && gridPos.y < _influenceMap.Height)
				{
					SetInfluence(gridPos, (Input.GetMouseButton(0) ? 1.0f : -1.0f));
				}
			}
		}
	}
}

[System.Serializable]
public struct BoundsI
{
	public Vector2I center,size, min, max;
	public BoundsI(Bounds b, InfluenceMapControl map){
		center = map.GetGridPosition(b.center);
		size = new Vector2I (0, 0, 0);
		size.x = (int) (b.size.x * map.GetGridSize ());
		size.y = (int)(b.size.z * map.GetGridSize ());
		min = map.GetGridPosition (b.min);
		max = map.GetGridPosition (b.max);
	}
	public bool Contains(Vector2I pos)
	{
		return pos.x >= min.x && pos.x <= max.x && pos.y >= min.y && pos.y <= max.y;
			
	}
}
