namespace Inventory.DTOs.Auth;
 public record UserToRegisterDto(string Email, string Password, string Name, string Phone);
 public record UserToLoginDto(string Email, string Password);
 public record UserToListDto(int Id, string Email, string Name, string Phone, string Token);