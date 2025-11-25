namespace DotNetTraining.MinimalApi.Endpoints.Products;

public static class ProductEndpoint
{
    public static void MapProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", () =>
        {
            string folderPath = "Data/products.json";
            var result = File.ReadAllText(folderPath);
            var items = JsonConvert.DeserializeObject<ProductResponse>(result);

            if (items is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(items);
        })
.WithName("GetProducts")
.WithOpenApi();

        app.MapGet("/products/{id}", (int id) =>
        {
            string folderPath = "Data/products.json";
            var result = File.ReadAllText(folderPath);
            var items = JsonConvert.DeserializeObject<ProductResponse>(result);

            if (items is null)
            {
                return Results.NotFound();
            }

            var item = items.products.FirstOrDefault(x => x.id == id);

            if (item is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(item);
        })
        .WithName("GetProductsById")
        .WithOpenApi();

        app.MapPost("/products", (Product product) =>
        {
            string folderPath = "Data/products.json";
            var result = File.ReadAllText(folderPath);
            var items = JsonConvert.DeserializeObject<ProductResponse>(result);

            var id = items.products.Count == 0 ? 1 : items.products.Max(x => x.id + 1);
            items.products.Add(product);

            string jsonToWrite = JsonConvert.SerializeObject(items);
            File.WriteAllText(folderPath, jsonToWrite);

            return Results.Ok(items);
        })
        .WithName("InsertProducts")
        .WithOpenApi();

        app.MapPut("/products/{id}", (int id, Product product) =>
        {
            string folderPath = "Data/products.json";
            var result = File.ReadAllText(folderPath);
            var items = JsonConvert.DeserializeObject<ProductResponse>(result);

            var findProducts = items.products.FirstOrDefault(x => x.id == id);
            if (findProducts is null)
            {
                return Results.NotFound();
            }

            if (!string.IsNullOrEmpty(product.thumbnail))
            {
                findProducts.thumbnail = product.thumbnail;
            }

            string jsonToWrite = JsonConvert.SerializeObject(items);
            File.WriteAllText(folderPath, jsonToWrite);

            return Results.Ok(items);
        })
        .WithName("UpdateProducts")
        .WithOpenApi();

    }
}

public class ProductResponse
{
    public List<Product> products { get; set; }
    public int total { get; set; }
    public int skip { get; set; }
    public int limit { get; set; }
}

public class Product
{
    public int id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string category { get; set; }
    public float price { get; set; }
    public float discountPercentage { get; set; }
    public float rating { get; set; }
    public int stock { get; set; }
    public string[] tags { get; set; }
    public string brand { get; set; }
    public string sku { get; set; }
    public int weight { get; set; }
    public Dimensions dimensions { get; set; }
    public string warrantyInformation { get; set; }
    public string shippingInformation { get; set; }
    public string availabilityStatus { get; set; }
    public Review[] reviews { get; set; }
    public string returnPolicy { get; set; }
    public int minimumOrderQuantity { get; set; }
    public Meta meta { get; set; }
    public string[] images { get; set; }
    public string thumbnail { get; set; }
}

public class Dimensions
{
    public float width { get; set; }
    public float height { get; set; }
    public float depth { get; set; }
}

public class Meta
{
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string barcode { get; set; }
    public string qrCode { get; set; }
}

public class Review
{
    public int rating { get; set; }
    public string comment { get; set; }
    public DateTime date { get; set; }
    public string reviewerName { get; set; }
    public string reviewerEmail { get; set; }
}
