using Fituska.BL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
var clientUrl = "https://localhost:7029";
#else
var clientUrl = "https://icy-flower-0d67f0203.azurestaticapps.net";
#endif
builder.Services.AddCors(options =>
{
    options.AddPolicy("FituskaCorsPolicy", policy =>
    {
        //policy.WithOrigins(clientUrl).AllowAnyHeader().AllowAnyMethod();
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<FituskaDbContext>(options =>
{
#if DEBUG
    options.UseSqlite(builder.Configuration.GetConnectionString("DesignTimeConnection"));
#else
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
#endif
});

builder.Services.AddDefaultIdentity<UserEntity>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<FituskaDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Fituška API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(EntityBase), typeof(ModelBase), typeof(UserMapperProfiles));

builder.Services.AddScoped<AnswerRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<CourseAttendanceRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<FileRepository>();
builder.Services.AddScoped<QuestionRepository>();
builder.Services.AddScoped<UserSawQuestionRepository>();
builder.Services.AddScoped<VoteRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    var roleManager = serviceProvider.GetService<RoleManager<IdentityRole<Guid>>>();
    var userManager = serviceProvider.GetService<UserManager<UserEntity>>();
    if (roleManager is not null && userManager is not null)
    {
        await SeedRolesAndUsers.Seed(roleManager, userManager);
    }
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fituška API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("FituskaCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => app.MapControllers());

app.Run();
