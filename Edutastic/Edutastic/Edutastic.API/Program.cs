using Edutastic.API.Repositories;
using Edutastic.API.Services;
using Supabase.Postgrest;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var supabaseUrl = builder.Configuration["Supabase:Url"] ?? throw new Exception("Supabase:Url not configured");
var supabaseKey = builder.Configuration["Supabase:JwtSecret"] ?? throw new Exception("Supabase:JwtSecret not configured");

builder.Services.AddScoped(_ => new Client(supabaseUrl, new ClientOptions
{
    Headers =
    {
        { "apikey", supabaseKey },
        { "Authorization", $"Bearer {supabaseKey}" },
        { "Prefer", "return=representation" }
    }
}));

// Repositories
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<QuestionRepository>();
builder.Services.AddScoped<GameRepository>();
builder.Services.AddScoped<GameAnswerRepository>();
builder.Services.AddScoped<EnrollmentRepository>();
builder.Services.AddScoped<AchievementRepository>();

// Services
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<CourseService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();