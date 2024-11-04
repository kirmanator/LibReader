using Dapper.FluentMap;
using LibraryAPI.Config;
using LibraryAPI.Models;
using LibraryAPI.Repositories.RUserInfo;
using LibraryAPI.Repositories.RUserRole;
using LibraryAPI.Repositories.RUserReview;
using LibraryAPI.Repositories.RBook;
using LibraryAPI.Repositories.RBookLoan;
using LibraryAPI.Services.SUserInfo;
using LibraryAPI.Services.SBookLoan;
using LibraryAPI.Services.SBook;
using LibraryAPI.Services.SUserReview;
using LibraryAPI.Services.SUserRole;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ConnectionContext>();

builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();

builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();

builder.Services.AddScoped<IUserReviewRepository, UserReviewRepository>();
builder.Services.AddScoped<IUserReviewService, UserReviewService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IBookLoanRepository, BookLoanRepository>();
builder.Services.AddScoped<IBookLoanService, BookLoanService>();

builder.Services.AddControllers();
builder.WebHost.UseUrls(new[] { "https://localhost:7158" });

FluentMapper.Initialize(config => {
    config.AddMap(new UserInfoMap());
    config.AddMap(new UserRoleMap());
    config.AddMap(new UserReviewMap());
    config.AddMap(new BookMap());
    config.AddMap(new BookLoanMap());
});

var app = builder.Build();


if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
options.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

if (app.Environment.IsDevelopment()) {
    app.Run();
} else {
    app.Run("https://127.0.0.1:7158");
}
