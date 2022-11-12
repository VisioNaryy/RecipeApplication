using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RecipeApplication.CustomTagHelpers;

[HtmlTargetElement("system-info")]
public class SystemInfoTagHelper : TagHelper
{
    private readonly HtmlEncoder _htmlEncoder;

    [HtmlAttributeName("add-machine")] public bool IncludeMachine { get; set; } = false;

    [HtmlAttributeName("add-os")] public bool IncludeOs { get; set; } = false;

    public SystemInfoTagHelper(HtmlEncoder htmlEncoder)
    {
        _htmlEncoder = htmlEncoder;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div class='container'";
        output.TagMode = TagMode.StartTagAndEndTag;

        var sb = new StringBuilder();

        if (IncludeMachine)
        {
            sb.Append(" <strong>Machine</strong> ");
            sb.Append(_htmlEncoder.Encode(Environment.MachineName));
        }

        if (IncludeOs)
        {
            sb.Append(" <strong>OS</strong> ");
            sb.Append(_htmlEncoder.Encode(RuntimeInformation.OSDescription));
        }

        output.Content.SetHtmlContent(sb.ToString());
    }
}