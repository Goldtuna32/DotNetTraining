namespace DotNetTraining.MinimalApi.Endpoints.Birds;

public static class BirdEndpoint
{
    public static void MapBirdEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/Birds", () =>
        {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdTable>(json)!;
            return Results.Ok(result.Tbl_Bird);
        })
.WithName("GetBirds")
.WithOpenApi();

        app.MapGet("/Birds/{id}", (int id) =>
        {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdTable>(json)!;
            var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(item);
        })
        .WithName("GetBirdsById")
        .WithOpenApi();

        app.MapPost("/blogs", (BirdModel birdModel) =>
        {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdTable>(json)!;

            var id = result.Tbl_Bird.Count == 0 ? 1 : result.Tbl_Bird.Max(x => x.Id) + 1;
            result.Tbl_Bird.Add(birdModel);

            string jsonWrite = JsonConvert.SerializeObject(result);
            File.WriteAllText(folderPath, jsonWrite);

            return Results.Ok();

        })
        .WithName("CreateBirds")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", (BirdModel birdModel) =>
        {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);
            var existingResult = JsonConvert.DeserializeObject<BirdTable>(json)!;

            existingResult.Tbl_Bird.Add(birdModel);

            string jsonWrite = JsonConvert.SerializeObject(existingResult);
            File.WriteAllText(folderPath, jsonWrite);

            return Results.Ok();

        })
        .WithName("UpdateBirds")
        .WithOpenApi();

        app.MapPatch("/blogs/{id}", (int id, BirdModel birdModel) =>
        {
            string folderPath = "Data/Birds.json";
            var json = File.ReadAllText(folderPath);
            var existingResult = JsonConvert.DeserializeObject<BirdTable>(json)!;

            var item = existingResult.Tbl_Bird.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return Results.NotFound();
            }

            if (!string.IsNullOrEmpty(birdModel.BirdMyanmarName))
            {
                item.BirdMyanmarName = birdModel.BirdMyanmarName;
            }

            if (!string.IsNullOrEmpty(birdModel.BirdEnglishName))
            {
                item.BirdEnglishName = birdModel.BirdEnglishName;
            }

            if (!string.IsNullOrEmpty(birdModel.Description))
            {
                item.Description = birdModel.Description;
            }
            if (!string.IsNullOrEmpty(birdModel.ImagePath))
            {
                item.ImagePath = birdModel.ImagePath;
            }

            existingResult.Tbl_Bird.Add(item);
            string jsonWrite = JsonConvert.SerializeObject(existingResult);
            File.WriteAllText(folderPath, jsonWrite);

            return Results.Ok();

        })
        .WithName("EditBirds")
        .WithOpenApi();
    }

    public class BirdTable
    {
        public List<BirdModel> Tbl_Bird { get; set; }
    }

    public class BirdModel
    {
        public int Id { get; set; }
        public string BirdMyanmarName { get; set; }
        public string BirdEnglishName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
