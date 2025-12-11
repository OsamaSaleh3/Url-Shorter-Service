using DataAccessLayer.Entity;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class UrlService
    {
        private const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int CodeLength = 8;

        private readonly UrlRepository _urlRepository;

        public UrlService(UrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        private string GenerateShortCode()
        {
            string s = "";
            for (int i = 0; i < CodeLength; i++)
            {
                s += Alphabet[Random.Shared.Next(Alphabet.Length)];
            }

            return s;
        }

        public async Task<string> GenerateUniqueShortUrl(string longUrl, CancellationToken ct)
        {
            string shortCode;
            bool isUnique = false;
            do
            {
                shortCode = GenerateShortCode();
                bool exist = await _urlRepository.CheckIfShortCodeExists(shortCode, ct);
                if (!exist)
                {
                    isUnique = true;
                }
            }
            while (!isUnique);

            var newUrl = new Url
            {
                LongUrl = longUrl,
                ShortCode = shortCode,
            };
            await _urlRepository.AddUrl(newUrl, ct);
            return shortCode;
        }

        public async Task<string?> GetLongUrl(string shortCode, CancellationToken ct)
        {
            var url = await _urlRepository.GetLongUrlByShortCode(shortCode, ct);
            return url;
        }
    }
}
