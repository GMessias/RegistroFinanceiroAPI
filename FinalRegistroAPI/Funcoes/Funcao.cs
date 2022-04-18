using FinalRegistroAPI.Models;
using System.Text.Json;

namespace FinalRegistroAPI.Funcoes
{
    public class Funcao
    {
        #region Abrir arquivo por requisição
        /// <summary>
        /// Abre o arquivo recebido por requisição.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Retorna: Stream</returns>
        public Stream AbrirArquivoPorRequisicao(ArquivoRequest request)
        {
            var stream = request.file.OpenReadStream();
            return stream;
        }
        #endregion

        #region Conversor de Stream para string
        /// <summary>
        /// Converte para string o conteúdo do objeto Stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>Retorna: string</returns>
        public string TransformarStreamEmString(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string texto = reader.ReadToEnd();

            return texto;
        }
        #endregion

        #region Execução de duas funções envolvendo Stream e string
        /// <summary>
        /// Executa duas funções: AbrirAbrirArquivoPorRequisicao e TransformarStreamEmString.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Retorna: string</returns>
        public string TodoProcessoStreamParaString(ArquivoRequest request)
        {
            return TransformarStreamEmString(AbrirArquivoPorRequisicao(request));
        }
        #endregion

        #region Leitura de um arquivo Json
        /// <summary>
        /// Leitura dum arquivo JSON que retornará em string o conteúdo do arquivo.
        /// </summary>
        /// <param name="jsonFile">String, necessário uma URL absoluta do arquivo JSON ou o próprio arquivo que virá do POST.</param>
        /// <returns>
        /// Retorna uma string. 
        /// </returns>
        public string LerArquivoJson(string jsonFile)
        {
            // @"C:\Users\gmess\OneDrive\Área de Trabalho\VS_Project\APIRestful\ConvertJSON\file.json"
            return File.ReadAllText(jsonFile);
        }
        #endregion

        #region JsonSerializer -> método Deserialize
        /// <summary>
        /// Separa uma string de formato JSON em objetos colocando dentro duma lista.
        /// </summary>
        /// <param name="jsonFile">String</param>
        /// <returns>Retorna: List<FileJsonModel></returns>
        public List<FileJsonModel> DeserializarArquivoJson(string jsonFile)
        {
            return JsonSerializer.Deserialize<List<FileJsonModel>>(jsonFile);
        }
        #endregion

        #region JsonSerializer -> método Serialize 
        /// <summary>
        /// Converte uma lista numa string em formato JSON.
        /// </summary>
        /// <param name="jsonFile">List<FileJsonModel></param>
        /// <returns>Retorna: String</returns>
        public string SerializarArquivoJson(List<FileJsonModel> jsonFile)
        {
            return JsonSerializer.Serialize(jsonFile);
        }

        /// <summary>
        /// Converte uma lista numa string em formato JSON.
        /// </summary>
        /// <param name="jsonFile">IOrderedEnumerable<FileJsonModel></param>
        /// <returns>Retorna: String</returns>
        public string SerializarArquivoJson(IOrderedEnumerable<FileJsonModel> jsonFile)
        {
            return JsonSerializer.Serialize(jsonFile);
        }
        #endregion

        #region Ordenar uma lista de forma crescente em relação ao mês
        /// <summary>
        /// Ordena toda a lista de forma crescente de acordo com valor do mês.
        /// </summary>
        /// <param name="jsonFile">List<FileJsonModel></param>
        /// <returns>Retorna: IOrderedEnumerable<FileJsonModel></returns>
        public IOrderedEnumerable<FileJsonModel> OrdenarListaCrescenteMesJson(List<FileJsonModel> jsonFile)
        {
            var orderByMes = jsonFile.OrderBy(x => x.DiaMesAno);
            return orderByMes;
        }
        #endregion

        #region GroupBy em relação aos meses
        /// <summary>
        /// Agrupa a lista de objetos de acordo com o mês.
        /// </summary>
        /// <param name="jsonFile">List<FileJsonModel></param>
        /// <returns>Retorna: List<List<FileJsonModel>></returns>
        public List<List<FileJsonModel>> AgruparPorMesJson(List<FileJsonModel> jsonFile)
        {
            var groupedCustomerList = jsonFile
                .GroupBy(u => u.DiaMesAno.Month)
                .Select(grp => grp.ToList())
                .ToList();

            return groupedCustomerList;
        }
        #endregion

        #region GroupBy em relação as categorias
        /// <summary>
        /// Agrupa a lista de objetos de acordo com as categorias.
        /// </summary>
        /// <param name="jsonFile">List<FileJsonModel></param>
        /// <returns>Retorna: List<List<FileJsonModel>></returns>
        public List<List<FileJsonModel>> AgruparPorCategoriaJson(List<FileJsonModel> jsonFile)
        {
            var groupedCustomerList = jsonFile
                .GroupBy(u => u.Categoria)
                .Select(grp => grp.ToList())
                .ToList();

            return groupedCustomerList;
        }
        #endregion

        #region Calcular preço total referente ao mês
        /// <summary>
        /// Calcula o preço gasto no total de acordo com o mês. Necessário utilizar o método para agrupar antes.
        /// </summary>
        /// <param name="jsonFile">List<List<FileJsonModel>></param>
        /// <returns>Retorna: Dictionary<int, double></returns>
        public Dictionary<int, double> CalcularPrecoPorMes(List<List<FileJsonModel>> jsonFile)
        {
            Dictionary<int, double> dictCalculoNoMes = new Dictionary<int, double>();

            for (int i = 0; i < jsonFile.Count; i++)
            {
                dictCalculoNoMes.Add(jsonFile[i][0].DiaMesAno.Month, 0.0);
                foreach (var uni in jsonFile[i])
                {
                    dictCalculoNoMes[jsonFile[i][0].DiaMesAno.Month] += uni.Preco;
                }
            }
            return dictCalculoNoMes;
        }
        #endregion

        #region Calcular preço total referente a categoria
        /// <summary>
        /// Calcula o preço gasto no total de acordo com a categoria. Necessário utilizar o método para agrupar antes.
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns>Retorna: Dictionary<string, double></returns>
        public Dictionary<string, double> CalcularPrecoPorCategoria(List<List<FileJsonModel>> jsonFile)
        {
            Dictionary<string, double> dictCalculoPorCategoria = new Dictionary<string, double>();

            for (int i = 0; i < jsonFile.Count; i++)
            {
                dictCalculoPorCategoria.Add(jsonFile[i][0].Categoria, 0.0);
                foreach (var uni in jsonFile[i])
                {
                    dictCalculoPorCategoria[jsonFile[i][0].Categoria] += uni.Preco;
                }
            }
            return dictCalculoPorCategoria;
        }
        #endregion

        #region Calcular o total de valor gasto
        /// <summary>
        /// Calcular o total de gastos sem distinção de categoria, mês, ano etc.
        /// </summary>
        /// <param name="jsonFile"></param>
        /// <returns>Retorna: double</returns>
        public double CalcularTotalDeGastos(List<FileJsonModel> jsonFile)
        {
            var total = 0.0;

            for (int i = 0; i < jsonFile.Count(); i++)
            {
                total += jsonFile[i].Preco;
            }

            return total;
        }
        #endregion
    }
}
