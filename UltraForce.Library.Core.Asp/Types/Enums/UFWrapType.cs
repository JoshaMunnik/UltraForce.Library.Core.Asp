namespace UltraForce.Library.Core.Asp.Types.Enums;

public enum UFWrapType
{
  /// <summary>
  /// No wrapping
  /// </summary>
  None,
  
  /// <summary>
  /// Normal wrapping (split at word boundaries)
  /// </summary>
  Normal,
  
  /// <summary>
  /// Split only long words, try to preserve smaller words 
  /// </summary>
  Word,
  
  /// <summary>
  /// Split all words
  /// </summary>
  All
}