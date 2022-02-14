using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Arkham.Onigiri.DebugModule
{
    [ExecuteInEditMode]
    public class DebugController : MonoBehaviour
    {

        [SerializeField] private DebugManager m_DebugManager;


        private Vector2 _scrollViewVector = Vector2.zero;



        private void OnGUI()
        {
            if (m_DebugManager == null)
            {
                GUI.Label(new Rect(0, 0, 100, 24), "No Manager");
                return;
            }

            if (GUI.Button(new Rect(m_DebugManager.margeLeft, m_DebugManager.margeTop, m_DebugManager.width, m_DebugManager.itemHeight), m_DebugManager.show ? "Hide" : "Show")) ToggleView();

            if (!m_DebugManager.show) return;

            float _y = m_DebugManager.margeTop + m_DebugManager.itemHeight;

            if (m_DebugManager.allVariables == null || m_DebugManager.allVariables.Length <= 0) return;

            float h = m_DebugManager.allEvents.Length * (m_DebugManager.itemHeight + m_DebugManager.itemSpacing);
            foreach (var item in m_DebugManager.allVariables) h += item.GetHeigh(m_DebugManager.itemHeight, m_DebugManager.itemSpacing);

            GUI.Box(new Rect(m_DebugManager.margeLeft - 20, _y, m_DebugManager.width + 40, h), "");

            _scrollViewVector = GUI.BeginScrollView(new Rect(m_DebugManager.margeLeft, _y, m_DebugManager.width + 40, Screen.height - m_DebugManager.margeTop), _scrollViewVector, new Rect(0, 0, m_DebugManager.width, h + m_DebugManager.itemHeight));

            _y = 0;
            foreach (var item in m_DebugManager.allVariables)
                _y = item.DrawGUI(m_DebugManager.width, m_DebugManager.itemHeight, m_DebugManager.itemSpacing, _y);

            foreach (var item in m_DebugManager.allEvents)
            {
                if (GUI.Button(new Rect(0, _y, m_DebugManager.width, m_DebugManager.itemHeight), item?.name)) item?.Raise();
                _y += m_DebugManager.itemHeight + m_DebugManager.itemSpacing;
            }

            GUI.EndScrollView();

#if UNITY_EDITOR
            if (!Application.isPlaying && m_DebugManager.updateInEditor)
            {
                EditorApplication.QueuePlayerLoopUpdate();
                SceneView.RepaintAll();
            }
#endif
        }

        [Button]
        public void ToggleView()
        {
            m_DebugManager.show = !m_DebugManager.show;
        }


    }
}
