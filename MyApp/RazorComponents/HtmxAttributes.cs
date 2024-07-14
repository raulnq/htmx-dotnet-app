namespace MyApp.RazorComponents;

public class HtmxAttributes
{
    public string Target { get; set; } = default!;
    public string Swap { get; set; } = "innerHTML";
    public string Endpoint { get; set; } = default!;
    public string Select { get; set; } = default!;
    public string Confirm { get; set; } = default!;
    public HtmxAttributes()
    {

    }

    public HtmxAttributes(string endpoint, string target)
    {
        Endpoint = endpoint;
        Target = target;
    }
    public HtmxAttributes(string endpoint, string target, string swap) : this(endpoint, target)
    {
        Swap = swap;
    }

    public HtmxAttributes(string endpoint, string target, string swap, string select) : this(endpoint, target, swap)
    {
        Select = select;
    }
}