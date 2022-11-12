using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RecipeApplication.CustomTagHelpers;

[HtmlTargetElement(Attributes = "if")]
public class IfTagHelper : TagHelper
{
    [HtmlAttributeName("if")] public bool RenderContent { get; set; } = true;
    
    public override int Order => int.MinValue;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!RenderContent)
        {
            output.TagName = null;
            output.SuppressOutput();
        }
    }
}