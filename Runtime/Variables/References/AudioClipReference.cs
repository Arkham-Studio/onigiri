using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [InlineProperty]
    [Serializable]
    [LabelWidth(75)]
    public class AudioClipReference : BaseVariableReference<AudioClip, AudioClipVariable>, IReferenceDrawer
    {
        public AudioClipReference(AudioClip value) : base(value)
        {
        }
    }
}