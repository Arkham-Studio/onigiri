using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Playables;

//  MONO
[System.Serializable]
public class GameObjectUnityEvent : UnityEvent<GameObject> { }

[System.Serializable]
public class TransformUnityEvent : UnityEvent<Transform> { }

[System.Serializable]
public class AnimatorUnityEvent : UnityEvent<Animator> { }

[System.Serializable]
public class RigidbodyUnityEvent : UnityEvent<Rigidbody> { }

[System.Serializable]
public class Rigidbody2DUnityEvent : UnityEvent<Rigidbody2D> { }

[System.Serializable]
public class PlayableDirectorUnityEvent : UnityEvent<PlayableDirector> { }


//  UI MONO
[System.Serializable]
public class ButtonUnityEvent : UnityEvent<Button> { }

[System.Serializable]
public class ToggleUnityEvent : UnityEvent<Toggle> { }


//  VARIABLES
[System.Serializable]
public class IntUnityEvent : UnityEvent<int> { }

[System.Serializable]
public class FloatUnityEvent : UnityEvent<float> { }

[System.Serializable]
public class LongUnityEvent : UnityEvent<long> { }

[System.Serializable]
public class BoolUnityEvent : UnityEvent<bool> { }

[System.Serializable]
public class StringUnityEvent : UnityEvent<string> { }

[System.Serializable]
public class DoubleUnityEvent : UnityEvent<double> { }


//  ONIGIRI
[System.Serializable]
public class DenumUnityEvent : UnityEvent<DenumVariable> { }

[System.Serializable]
public class BaseVariableUnityEvent<T> : UnityEvent<T> { }