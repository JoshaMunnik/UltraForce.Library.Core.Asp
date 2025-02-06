using UltraForce.Library.Core.Asp.Types.Enums;

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
  
  /// <summary>
  /// How to position this item horizontally within the container.
  /// </summary>
  UFContentPosition Horizontal { get; set; }

  /// <summary>
  /// How to position this item vertically within the container.
  /// </summary>
  UFContentPosition Vertical { get; set; }
}