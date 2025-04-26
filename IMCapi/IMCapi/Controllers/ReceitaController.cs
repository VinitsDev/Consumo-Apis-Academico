using Microsoft.AspNetCore.Mvc;

namespace IMCapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceitaController : ControllerBase
    {
        private static List<Receita> receitas = new List<Receita>
        {
            new Receita { ReceitaId = 1, NomeReceita = "Salada Caesar", Tipo = "Fit", Ingredientes = "Frango, Alface e Molho" },
            new Receita { ReceitaId = 2, NomeReceita = "Parmegiana de Frango", Tipo = "Normal", Ingredientes = "Frango, Molho de tomate e Farinha" },
            new Receita { ReceitaId = 3, NomeReceita = "Milk Shake", Tipo = "Normal", Ingredientes = "Leite, Açucar e Chocolate" },
            new Receita { ReceitaId = 4, NomeReceita = "Pizza", Tipo = "Normal", Ingredientes = "Molho de tomate, Farinha e Queijo" },
        };

        private readonly ILogger<ReceitaController> _logger;

        public ReceitaController(ILogger<ReceitaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{imc}", Name = "GetReceita")]
        public IActionResult Get(double imc)
        {

            Random rand = new Random();
            if (imc < 25)
            {
                var receitasNormal = receitas.Where(r => r.Tipo == "Normal").ToList();

                Receita receitaAleatoria = receitasNormal[rand.Next(receitasNormal.Count)];

                return new JsonResult(receitaAleatoria);

            }
            else
            {
                var receitasFit = receitas.Where(r => r.Tipo == "Fit").ToList();

                Receita receitaAleatoria = receitasFit[rand.Next(receitasFit.Count)];

                return new JsonResult(receitaAleatoria);
            }
        }

        [HttpGet]
        public IActionResult ListarTodas()
        {
            return Ok(receitas);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Receita novaReceita)
        {
            if (novaReceita == null || string.IsNullOrEmpty(novaReceita.NomeReceita))
            {
                return BadRequest("Dados Inválidos!");
            }

            int novoId = receitas.Max(r => r.ReceitaId) +1;
            novaReceita.ReceitaId = novoId;
            receitas.Add(novaReceita);

            return Ok(novaReceita);
        }

        [HttpPut("id")]
        public IActionResult Put(int id, [FromBody] Receita receitaAtualizada)
        {
            if (receitaAtualizada == null)
            {
                return BadRequest("Dados inválidos");
            }

            var receitasExistente = receitas.FirstOrDefault(r => r.ReceitaId == id);
            if (receitasExistente == null)
            {
                return NotFound("Receita não encontrada.");
            }

            receitasExistente.NomeReceita = receitaAtualizada.NomeReceita;
            receitasExistente.Tipo = receitaAtualizada.Tipo;
            receitasExistente.Ingredientes = receitaAtualizada. Ingredientes;

            return Ok(receitasExistente);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var receita = receitas.FirstOrDefault(r => r.ReceitaId==id);
            if (receita == null)
            {
                return NotFound("Receita não encontrada");
            }
            receitas.Remove(receita);
            return NoContent();

        }

    }
}
