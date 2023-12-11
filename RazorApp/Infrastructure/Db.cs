using RazorApp.Pages.Products;

namespace RazorApp.Infrastructure;

public static class Db
{
    public static List<Product> Products { get; } = new()
    {
        new Product(
            Name: "Laptop",
            Id: "1",
            Description: "Powerful laptop with high-performance specifications.",
            Price: 1200.99),
        new Product(
            Name: "Smartphone",
            Id: "2",
            Description: "Latest smartphone with advanced camera features.",
            Price: 799.99),
        new Product(
            Name: "Headphones",
            Id: "3",
            Description: "Noise-canceling headphones for immersive audio experience.",
            Price: 149.99),
        new Product(
            Name: "Name: Smartwatch",
            Id: "4",
            Description: "Fitness tracker with heart rate monitoring and GPS.",
            Price: 199.99),
        new Product(
            Name: "Camera",
            Id: "5",
            Description: "Professional DSLR camera for capturing stunning photos.",
            Price: 1499.99),
        new Product(
            Name: "Gaming Console",
            Id: "6",
            Description: "Next-gen gaming console for an ultimate gaming experience.",
            Price: 499.99),
        new Product(
            Name: "Tablet",
            Id: "7",
            Description: "Lightweight tablet with a high-resolution display.",
            Price: 299.99),
        new Product(
            Name: "Bluetooth Speaker",
            Id: "8",
            Description: "Portable Bluetooth speaker with rich sound quality.",
            Price: 79.99),
        new Product(
            Name: "External Hard Drive",
            Id: "9",
            Description: "High-capacity external hard drive for data storage.",
            Price: 129.99),
        new Product(
            Name: "Wireless Mouse",
            Id: "10",
            Description: "Ergonomic wireless mouse for comfortable use.",
            Price: 29.99)
    };
}
