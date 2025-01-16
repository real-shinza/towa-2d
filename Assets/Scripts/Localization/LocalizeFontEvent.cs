using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;

[AddComponentMenu("Localization/Asset/" + nameof(LocalizeFontEvent))]
public class LocalizeFontEvent : LocalizedAssetEvent<Font, LocalizedFont, UnityEventFont> { }

[Serializable]
public class UnityEventFont : UnityEvent<Font> { }
