## Documentation
https://joshamunnik.github.io/UltraForce.Library.Core.Asp/index.html

## Resources in the package

Some tag helpers use styles defined in `uf-styles.css`.

Include the styles in your html using (for example `Shared\_Layout.cshtml`):
````html
<link rel="stylesheet" href="/_content/UltraForce.Library.Core.Asp/css/uf-styles.css" />
````
## Version history
1.0.90
- added UFBasicFlexTagHelperBase  

1.0.89
- UFMvcTools.CreateListFromEnum now uses the display name and not the description
 
1.0.88
- setting width to 1px when specifying min-width or max-width in UFTableCellTagHelperBase

1.0.87
- Renamed UFTableCelLTagHelperBase.Width to UFTableCellTagHelperBase.MinWidth and it sets
  the min-width style attribute.

1.0.86
- Replaced SetHtmlContent with AppendHtml on all PreContent, PostContent, PreElement and 
  PostElement calls.
- Removed no caching option from UFTableCellTagHelperBase

1.0.85
- added name parameter to GetValidationFeedbackContainerHtml in UFInputTagHelperBase and 
  UFSelectTagHelperBase

1.0.84
- added UFAttributesTagHelper
 
1.0.83
- added RedirectToIndex, RedirectToIndexWithId, RedirectToAction to UFController

1.0.82
- UFModelExpressionRenderer uses the display name and not description with enum values.

1.0.80
- fixed bug in uf-styles.css

1.0.79
- added SizeEvenly to UFFlexDistributeContentEnum

1.0.78
- added IsEnumType and GetEnumValues to UFModelExpressionTools

1.0.77
- added GetAttribute to UFModelExpressionTools

1.0.75
- added UFTableButtonTagHelperBase
- added UFTableChildTagHelperBase
- removed the 'a(n)' prefix from all parameter names

1.0.74
- added isStatic to UFButtonTagHelperBase protected methods

1.0.71
- added GetTableContainerClasses to UFTableTagHelperBase

1.0.70
- updated UFDataItemTagHelperBase

1.0.68
- added Name property to UFDataItemTagHelperBase

1.0.66
- added UFClearViewDataServiceFilter

1.0.65
- added UFDataTagHelperBase, UFDataTitleTagHelper, IUFViewDataService and UFViewDataService

1.0.64
- fixed bug, href should no longer be set with when a button or div tag is rendered

1.0.63
- fixed bug with rendering checkbox and radio buttons in UFInputTagHelperBase

1.0.61
- add MaxWidth to UFTableCellTagHelperBase
- MaxWidth and Width are added to style tag and no longer are processed as classes

1.0.57
- set title attribute with table data cells when For is being used.
 
1.0.56
- removes href attribute from UFButtonTagHelperBase when none could be determined.
 
1.0.52
- added GetBodyClasses and GetHeadClasses to UFTableTagHelperBase; the class adds thead and tbody 
  tags.
 
1.0.51
- added GetFilterPlaceholder and GetFilterCaptionHtml to UFTableTagHelperBase
 
1.0.50
- renamed UFCellTagHelperBase to UFTableCellTagHelperBase 
- converted UFTableRowTagHelperBase and UFTableCellTagHelperBase to generic classes

1.0.49
- added hasCaption parameter to UFButtonTagHelperBase.GetButtonClasses
 
1.0.48
- Fixed bug in UFViewdataTagHelper

1.0.47
- Added UFViewdataTagHelper 

1.0.46
- Added hasContent parameter to GetBeforeCaptionHtml and GetAfterCaptionHtml in UFInputTagHelperBase

1.0.45
- moved version info to README.md
- added UFUseActionException and UFUseActionExceptionFilter

1.0.44
- removed HtmlAttribute annotation from base TagHelpers

1.0.43
- added GetLabelHtmlAsync to UFInputTagHelperBase

1.0.42
- enabled error container below checkbox and radio.

1.0.41
- updated UFFontAwesome.cs to icon names from v6.7.2

1.0.40
- added SessionStorage to UFController
- UFSessionKeyStorage uses System.Text.Json to store and retrieve objects

1.0.39
- added Reverse property to UFFlexTagHelperBase

1.0.38
- added tag helpers for layout
- Renamed and move base tag helpers

1.0.33
- add pre and post html methods to UFInputTagHelper for text inputs.

1.0.32
- added UFStyledTagHelperBase

1.0.31
- removed styling related code and types
- removed IUFTheme and classes implementing this interface
- removed tag helpers that only applied some styling
- most tag helpers are now abstract; subclasses can override any of the protected virtual methods

1.0.30
- renamed color to type attribute in UFButtonTagHelper

1.0.29
- renamed some theme methods to include TextInput

1.0.28
- added Text to UFButtonType

1.0.27
- added content position to IUFStackItem and UFStackItemTagHelper

1.0.26
- updated tag helpers

1.0.25
Updated comments.

1.0.24
Added properties to IUFButtonProperties

1.0.23
Fixed bug: using correct tag names for uf-stack and uf-stack-item

1.0.22
Fixed bug: stack styling methods are now virtual in UFTheme

1.0.21
Added <uf-stack> and <uf-stack-item>
Added UFContentPosition.Stretch

1.0.20
Added UFFlexJustifyContent.Stretch

1.0.19
Added UFButtonVariant.Auto and use it as default value

1.0.18
Included source documentation

1.0.17
UFClickableTagHelper now is a subclass of AnchorTagHelper
Breaking change: "controller", "action", "route-" are now obsolete; use "asp-..." instead
Added "call" and "call-parameter-separator" to UFClickableTagHelper

1.0.16
UFButtonTagHelper updates the color based on the button state.

1.0.15
If UFButonTagHelper.Disabled is true, an anchor is rendered as a div
Added Auto to UFButtonColor and UFButtonIconPosition

1.0.14
Added support for data-uf-no-caching with tables.

1.0.13
UFCells sort-type is now also processed if for attribute is not used.

1.0.12
Bug fix: clear button with table filter clears input field

1.0.10
Added Submit to IUFButtonProperties
Renamed Interactive to Static in IUFButtonProperties and swapped its functionality

1.0.9
IUFCellProperties.Wrap is now using UFWrapType

1.0.8
Renamed IUFTableCellProperties.Filter to NoFilter
UFCellTagHelper.Wrap is now false when not set

1.0.7
Removed label with table filter (using placeholder)
Removed table filter label related call in IUFTheme

1.0.6
Removed UFButtonStyle and related properties
Added UFFontAwesome

1.0.5
Added UFDataItemTagHelper

1.0.4
Attributes are inserted before the original attributes.

1.0.3
UFTableRowTagHelper Hover and Alternate are false by default

1.0.2
Removed UFSession middleware
Removed IUFSessionMessages; register UFSessionMessages as a scoped service.

1.0.1
Added try/catch when accessing the session.

1.0.0
Initial version