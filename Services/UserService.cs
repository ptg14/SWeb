﻿using System.Net.Mail;
using SWeb.Exceptions;
using SWeb.Models;
using SWeb.Repositories;
using SWeb.ViewModels;
using System.Net;
using Konscious.Security.Cryptography;
using System.Text;

namespace SWeb.Services
{
    public class UserService
    {
        private UserRepository _userRepository;
        private ImageService _imageService;

        public UserService(UserRepository userRepository, ImageService imageService)
        {
            _userRepository = userRepository;
            _imageService = imageService;
        }

        public async Task<List<UserFriendViewModel>> GetAllUsers()
        {
            List<User> users = await _userRepository.GetAllUsers();
            //convert users to user friend view models
            List<UserFriendViewModel> userFriendViewModels = new List<UserFriendViewModel>();
            foreach (User user in users)
            {
                UserFriendViewModel userFriendViewModel = new UserFriendViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    ProfileImageUrl = user.ProfileImageUrl
                };
                userFriendViewModels.Add(userFriendViewModel);
            }
            return userFriendViewModels;
        }

        private string HashPassword(string password)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = Encoding.UTF8.GetBytes("mynameisduongvanthang"),
                DegreeOfParallelism = 8,
                MemorySize = 1024 * 1024,
                Iterations = 4
            };

            return Convert.ToBase64String(argon2.GetBytes(16));
        }

        public async Task Register(RegisterUserViewModel user, HttpRequest httpRequest)
        {
            User? foundUser = await _userRepository.GetUserByEmail(user.Email);
            if (foundUser != null)
            {
                throw new AlreadyExistesException("Email already exists.");
            }

            User convertedUser = new User
            {
                Email = user.Email,
                Name = user.Name,
                Password = HashPassword(user.Password),
                Address = user.Address,
                Phone = user.Phone
            };

            await _userRepository.AddUser(convertedUser);
            try
            {
                await SendConfirmationEmail(convertedUser, httpRequest);
            }
            catch (Exception e)
            {
                await _userRepository.DeleteUser(convertedUser);
                throw new MailConfirmationException(e.Message);
            }

        }
        private async Task SendConfirmationEmail(User user, HttpRequest httpRequest)
        {
            String verificationLink = httpRequest.Scheme + "://" + httpRequest.Host + "/api/v1/users/verify/" + user.Id;

            string htmlBody = $@"
        <html>
        <body>
            <h1>Complete Your Registration</h1>
            <p>Click the button below to complete your registration:</p>
            <a href='{verificationLink}' style='background-color: blue; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Enter here to register</a>
        </body>
        </html>";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("SWeb@gmail.com");
                mail.To.Add(user.Email);
                mail.Subject = "Complete Your Registration";
                mail.Body = htmlBody;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    // Retrieve username and password from environment variables
                    string? username = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
                    string? password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");

                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = Encoding.UTF8.GetBytes("mynameisduongvanthang"),
                DegreeOfParallelism = 8,
                MemorySize = 1024 * 1024,
                Iterations = 4
            };

            var hashToCompare = Convert.ToBase64String(argon2.GetBytes(16));
            return hashToCompare == hashedPassword;
        }

        public async Task<ReturnedUserView> Login(LoginUserViewModel user)
        {

            User? foundUser = await _userRepository.GetUserByEmail(user.Email);
            if (foundUser == null)
            {
                throw new NotFoundException("Credentials are wrong!");
            }

            bool matchingPassword = VerifyPassword(user.Password, foundUser.Password);
            if (!matchingPassword)
            {
                throw new InvalidException("Credentials are wrong!");
            }

            if (!foundUser.EmailConfirmed)
            {
                throw new InvalidException("Email is not confirmed!");
            }
            ReturnedUserView returnedUserView = new ReturnedUserView
            {
                Id = foundUser.Id,
                Name = foundUser.Name,
                Email = foundUser.Email,
                Phone = foundUser.Phone,
                Address = foundUser.Address,
                ProfileImageUrl = foundUser.ProfileImageUrl
            };
            return returnedUserView;

        }

        public async Task Verify(int id)
        {

            User? foundUser = await _userRepository.GetUserById(id);
            if (foundUser == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            if (foundUser.EmailConfirmed)
            {
                throw new InvalidOperationException("Email already confirmed!");
            }

            foundUser.EmailConfirmed = true;
            await _userRepository.UpdateUserConfirmation(foundUser);


        }
        public async Task<string?> UpdateUser(int id, UpdateUserViewModel user)
        {
            User? foundUser = await _userRepository.GetUserById(id);
            if (foundUser == null)
            {
                throw new NotFoundException("User not found!");
            }
            if (user.Name != null)
                foundUser.Name = user.Name;
            if (user.Password != null)
                foundUser.Password = HashPassword(user.Password);
            if (user.Phone != null)
                foundUser.Phone = user.Phone;
            if (user.Address != null)
                foundUser.Address = user.Address;
            string? profileImageUrl = null;
            if (user.ProfileImage != null)
            {
                profileImageUrl = await _imageService.UploadImage(user.ProfileImage);
                foundUser.ProfileImageUrl = profileImageUrl;
            }

            await _userRepository.UpdateUser(id, foundUser);
            profileImageUrl = foundUser.ProfileImageUrl;
            return profileImageUrl;
        }
    }
}
