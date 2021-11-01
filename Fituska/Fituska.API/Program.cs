using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    //TODO: More restrictive policy for Azure with #if DEBUG #endif ?
    options.AddPolicy("FituskaCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//TODO: Change to UseSqlServer
builder.Services.AddDbContext<FituskaDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DesignTimeConnection"));
});

builder.Services.AddDefaultIdentity<UserEntity>()
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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var serviceProvider = builder.Services.BuildServiceProvider())
    {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole<Guid>>>();
        var userManager = serviceProvider.GetService<UserManager<UserEntity>>();
        if (roleManager is not null && userManager is not null)
        {
            await SeedRolesAndUsers.Seed(roleManager, userManager);
        }
    }
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fituška API v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("FituskaCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => app.MapControllers());

app.Run();
