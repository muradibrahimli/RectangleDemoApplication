﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using Containers;
using Microsoft.EntityFrameworkCore;

namespace RectangleApi.Attributes
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly InMemoryDbContext _dbContext; // Add a field for the database context

        // Modify the constructor to inject the database context
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            InMemoryDbContext dbContext) // Add IDbContext parameter
            : base(options, logger, encoder, clock)
        {
            _dbContext = dbContext; // Initialize the database context field
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                // Query the InMemoryDb to validate the username and password
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return AuthenticateResult.Fail("Invalid Username or Password");
                }

                var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username),
            };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }

}
