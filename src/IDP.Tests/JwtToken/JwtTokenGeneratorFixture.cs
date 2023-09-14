using Bound.IDP.Managers.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace Bound.IDP.Tests.JwtToken
{
    public class JwtTokenGeneratorFixture
    {
        public JwtTokenGenerator JwtTokenGenerator { get; set; }
        public string Token { get; set; }

        public JwtTokenGeneratorFixture()
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var jwtTokenGenerator = new JwtTokenGenerator(jwtSecurityTokenHandler);

            JwtTokenGenerator = jwtTokenGenerator;

            Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJpc3MiOiJodHRwczovL2JvdW5kdGVjaG5vbG9naWVzYWRiMmMuYjJjbG9naW4uY29tLzFiYzU0MzlkLTU1MmMtNGQ4Yy1hMTUzLTVhMTllMTc5MDczNC92Mi4wLyIsImV4cCI6MTYxMzU5MDMzOCwibmJmIjoxNjEzNTg2NzM4LCJhdWQiOiIxMjMwMTQ3Yi1lNmVmLTQ4ZDgtOTJkYy0yZGViODI3MTEyMWQiLCJpZHAiOiJMb2NhbEFjY291bnQiLCJvaWQiOiIzNDgxMmJkNi1jOGYxLTQ0ZjEtYjEzNy0yZDM3MjQ2MTEwZGEiLCJzdWIiOiIzNDgxMmJkNi1jOGYxLTQ0ZjEtYjEzNy0yZDM3MjQ2MTEwZGEiLCJnaXZlbl9uYW1lIjoidXNlciIsImZhbWlseV9uYW1lIjoidXNlciIsIm5hbWUiOiJ1c2VyQGJvdW5kdGVjaG5vbG9naWVzLmNvbSIsIm5ld1VzZXIiOmZhbHNlLCJjaXR5IjoidXNlciIsImNvdW50cnkiOiJ1c2VyIiwiam9iVGl0bGUiOiJ1c2VyIiwicG9zdGFsQ29kZSI6InVzZXIiLCJzdGF0ZSI6InVzZXIiLCJzdHJlZXRBZGRyZXNzIjoidXNlciIsImV4dGVuc2lvbl9Sb2xlIjoiVXNlciIsImVtYWlscyI6WyJ1c2VyQGJvdW5kdGVjaG5vbG9naWVzLmNvbSJdLCJ0ZnAiOiJCMkNfMV9ib3VuZHVzZXJmbG93IiwiYXpwIjoiMTIzMDE0N2ItZTZlZi00OGQ4LTkyZGMtMmRlYjgyNzExMjFkIiwidmVyIjoiMS4wIiwiaWF0IjoxNjEzNTg2NzM4fQ.eBJ0LtBPgiixnl5WRcOv32E95lK6ZqQ32bb_2QXAaiadCxBp9MBXgZc1AjbKDmCP_uvLOnPWn2v3ggY2nh4jgsDdn6vCafDX-XrXRAnZiVbI2RlNVLlUa907vAODvyBhTGV1IA0AltbEYNGJmd2Pg4cnrVLoiOSNY1k3g9SmQdZUUPA2t7R8qUMVsE7YT5yhMCX3Llq8xefnB5_wxEOvL9XzMo6qtGiFXY_svMH8xuzxLS83_Zy2Oj-ELXqmFAbcH-Z3cbEj7hP1-vJuYnfy_CGNoQ7GbE4zjI1BFUDlKRE3-YoxizGlpn59FxhxchVDJRwom2B-FiYjHC-bid-cmw";
        }
    }
}
