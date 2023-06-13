
using System.Text;
using interview.Helper;
using interview.Database;
using interview.Services;
using interview.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opt=>
opt.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection")));

builder.Services.AddTransient<IUserRepository,UserRepository>();
builder.Services.AddTransient<INoteRepository,NoteRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var _authkey = builder.Configuration.GetValue<string>("JwtSettings:securityKey");
builder.Services.AddAuthentication(item=>{
    item.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item=>{
    item.RequireHttpsMetadata = true;
    item.SaveToken =true;
    item.TokenValidationParameters = new TokenValidationParameters(){
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
        ValidateIssuer=false,
        ValidateAudience=false,
        ClockSkew =TimeSpan.Zero
    };
});

var _jwtsetting = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(_jwtsetting);

builder.Services.AddCors(opt=>opt.AddDefaultPolicy(des=>{
    des.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
