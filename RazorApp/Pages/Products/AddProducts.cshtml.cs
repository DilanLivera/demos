using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Infrastructure;
using RazorApp.Pages.Products;

namespace RazorApp.Pages;

[BindProperties]
public class AddProducts : PageModel
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public double Price { get; set; }

    public Task<IActionResult> OnPostAsync() => ModelState.IsValid
        ? AddProductAsync()
        : Task.FromResult<IActionResult>(Page());

    private Task<IActionResult> AddProductAsync()
    {
        var id = (Db.Products.Count + 1).ToString();
        var newProduct = new Product(Name, id, Description, Price);
        Db.Products.Add(newProduct);

        return Task.FromResult<IActionResult>(RedirectToPage("/Products/Products"));
    }
}
