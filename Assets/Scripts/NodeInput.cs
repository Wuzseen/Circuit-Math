using UnityEngine;
using System.Collections;

public interface NodeInput {
	void OnClick();

	void OnDrag(Vector2 delta);
}
