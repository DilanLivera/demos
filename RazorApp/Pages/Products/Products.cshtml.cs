using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Infrastructure;

namespace RazorApp.Pages.Products;

public class ProductsModel : PageModel
{
    public IReadOnlyList<Product> Products { get; private set; } = ImmutableList<Product>.Empty;

    public string Name => (string?)TempData[nameof(Name)] ?? "";

    public void OnGet() => Products = Db.Products.AsReadOnly();

    // public IActionResult OnPost([FromForm] string name)
    // {
    //     TempData[nameof(Name)] = name;
    //
    //     return RedirectToPage("Products");
    // }
}

public record Product(string Name, string Id, string Description, double Price);
