using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages;

public class ProductsModel : PageModel
{
    public IReadOnlyList<Product> Products { get; private set; } = ImmutableList<Product>.Empty;

    public void OnGet() => Products = new List<Product>
    {
        new(Name: "Product A", Id: Guid.NewGuid().ToString()),
        new(Name: "Product B", Id: Guid.NewGuid().ToString()),
        new(Name: "Product C", Id: Guid.NewGuid().ToString()),
        new(Name: "Product D", Id: Guid.NewGuid().ToString()),
        new(Name: "Product E", Id: Guid.NewGuid().ToString()),
        new(Name: "Product F", Id: Guid.NewGuid().ToString())
    };

    /*
    public string Name => (string?)TempData[nameof(Name)] ?? "";
    public IActionResult OnPost([FromForm] string name)
    {
        TempData[nameof(Name)] = name;

        return RedirectToPage("Products");
    }
    */
}

public record Product(string Name, string Id);
