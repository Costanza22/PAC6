﻿using FluentEmail;
using PAC6.API.DTO;
using PAC6.API.Interfaces;
using PAC6.API.Providers;
using PAC6.API.Requests;
using System.Net;
using System.Text.RegularExpressions;

namespace PAC6.API.Application
{
    public class CreateEmailApplication : ICreateEmailApplication
    {
        private readonly FirebaseConnectionProvider _firebase;
        private readonly string _regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public CreateEmailApplication(FirebaseConnectionProvider firebase)
        {
            _firebase = firebase;
        }

        public async Task<bool> Handle(CreateEmailCommand command)
        {
            command.Email = (command.Email ?? "").Trim();

            if (Regex.IsMatch(command.Email, _regex, RegexOptions.IgnoreCase))
            {
                var response = await _firebase.Connection.PushAsync("Emails/", new EmailDTO()
                {
                    Email = command.Email,
                    CreatedAt = DateTime.Now
                });

                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
