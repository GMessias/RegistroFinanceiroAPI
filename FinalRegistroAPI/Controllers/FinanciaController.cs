using FinalRegistroAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FinalRegistroAPI.Funcoes;

namespace FinalRegistroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinanciaController : ControllerBase
    {
        [HttpPost("registro")]
        public string EnviarArquivo([FromForm] ArquivoRequest request)
        {
            return request.file.FileName;
        }

        [HttpPost("retorno")]
        public List<FileJsonModel> Ler([FromForm] ArquivoRequest request)
        {
            Funcao funcoes = new Funcao();
            
            return funcoes.DeserializarArquivoJson(funcoes.TodoProcessoStreamParaString(request));
        }

        [HttpPost("ordenar-mes")]
        public IOrderedEnumerable<FileJsonModel> OrdenarPorMes([FromForm] ArquivoRequest request)
        {
            Funcao funcoes = new Funcao();
            var textoDeserializado = funcoes.DeserializarArquivoJson(funcoes.TodoProcessoStreamParaString(request));

            return funcoes.OrdenarListaCrescenteMesJson(textoDeserializado);
        }

        [HttpPost("grupo-mes")]
        public List<List<FileJsonModel>> AgruparPorMes([FromForm] ArquivoRequest request)
        {
            Funcao funcoes = new Funcao();
            var textoDeserializado = funcoes.DeserializarArquivoJson(funcoes.TodoProcessoStreamParaString(request));

            return funcoes.AgruparPorMesJson(textoDeserializado);
        }

        [HttpPost("grupo-categoria")]
        public List<List<FileJsonModel>> AgruparPorCategoria([FromForm] ArquivoRequest request)
        {
            Funcao funcoes = new Funcao();
            var textoDeserializado = funcoes.DeserializarArquivoJson(funcoes.TodoProcessoStreamParaString(request));

            return funcoes.AgruparPorCategoriaJson(textoDeserializado);
        }

        [HttpPost("gasto-mes")]
        public Dictionary<int, double> CalcularPrecoPorMes([FromForm] ArquivoRequest request)
        {
            Funcao funcoes = new Funcao();
            var textoDeserializado = funcoes.DeserializarArquivoJson(funcoes.TodoProcessoStreamParaString(request));

            return funcoes.CalcularPrecoPorMes(funcoes.AgruparPorMesJson(textoDeserializado));
        }

        [HttpPost("gasto-categoria")]
        public Dictionary<string, double> CalcularPrecoPorCategoria([FromForm] ArquivoRequest request)
        {
            Funcao funcoes = new Funcao();
            var textoDeserializado = funcoes.DeserializarArquivoJson(funcoes.TodoProcessoStreamParaString(request));

            return funcoes.CalcularPrecoPorCategoria(funcoes.AgruparPorCategoriaJson(textoDeserializado));
        }

        [HttpPost("gasto-total")]
        public double CalcularTotalDeGastos([FromForm] ArquivoRequest request)
        {
            Funcao funcoes = new Funcao();
            var textoDeserializado = funcoes.DeserializarArquivoJson(funcoes.TodoProcessoStreamParaString(request));

            return funcoes.CalcularTotalDeGastos(textoDeserializado);
        }
    }
}
