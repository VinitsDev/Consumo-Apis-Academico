using Microsoft.AspNetCore.Mvc;

namespace FraseMotivacional.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FrasesMotivacionaisController : ControllerBase
    {
        private static readonly string[] FrasesMotivacionais = new[]
        {
            "Acredite em si!", "Força Vasco!", "Tu consegue!", "Não é fácil!", "To sofrendo!", "Estudando demaisss", "Paula me dá 10"
        };

        private readonly ILogger<FrasesMotivacionaisController> _logger;

        public FrasesMotivacionaisController(ILogger<FrasesMotivacionaisController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetFraseMotivacional")]
        public FraseMotivacional Get()
        {
            return new FraseMotivacional
            {
                id = 1,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Frase = FrasesMotivacionais[Random.Shared.Next(FrasesMotivacionais.Length)]
            };
        }
    }
}
