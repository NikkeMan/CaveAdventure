using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class RuleTileLava : RuleTile<RuleTileLava.Neighbor> {
	public int siblingGroup;
	public class Neighbor : RuleTile.TilingRule.Neighbor {
		public const int Sibling = 3;
	}
	public override bool RuleMatch(int neighbor, TileBase tile) {
		RuleTileLava myTile = tile as RuleTileLava;
		switch (neighbor) {
			case Neighbor.Sibling: return myTile && myTile.siblingGroup == siblingGroup;
		}
		return base.RuleMatch(neighbor, tile);
	}
}