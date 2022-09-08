using MongoDB.Driver;
using UML.Models;

string connectionString = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
string databaseName = "diagram_db";
string collectionName = "diagrams";

var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
var collection = db.GetCollection<DiagramModel>(collectionName);

var diagram = new DiagramModel { serial = "123", username = "Test", screen = new ScreenModel[] { new ScreenModel { name = "Beginning", type = 0, xy = "456123" } } };



await collection.InsertOneAsync(diagram);

string connectionString2 = "mongodb+srv://CShark:5wulj7CrF1FTBpwi@umldb.7hgm9e0.mongodb.net/?retryWrites=true&w=majority";
var databaseName2 = "relation_db";
var collectionName2 = "relations";
var client2 = new MongoClient(connectionString2);
var db2 = client2.GetDatabase(databaseName2);
var collection2 = db.GetCollection<RelationsModel>(collectionName2);
var relations = new RelationsModel { serial = "123", singleRelation = new SingleRelationModel[] { new SingleRelationModel { type = 0, start = "456123", end = "789456" } } };
await collection2.InsertOneAsync(relations);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
