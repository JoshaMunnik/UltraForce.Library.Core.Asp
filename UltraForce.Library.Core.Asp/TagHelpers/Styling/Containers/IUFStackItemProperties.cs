namespace UltraForce.Library.Core.Asp.TagHelpers.Styling.Containers;

/// <summary>
/// Properties for a stack item.
/// </summary>
public interface IUFStackItemProperties
{
  /// <summary>
  /// When true prevent interaction with the stack item.
  /// </summary>
  public bool NoInteraction { get; set; }
}