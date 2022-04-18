# API Financeiro

Apesar do nome, não tem nenhuma funcionalidade para gravar, de alguma forma, os dados enviados.</br>

É predominante a requisição POST.</br>

<strong>É necessário um arquivo JSON</strong>, seu formato é este:</br>
ˋˋˋ
	[
		{
			"DiaMesAno": "2022-02-04",
			"Categoria": "Supermercado",
			"Preco": 300.00
		},
		{
			"DiaMesAno": "2022-02-23",
			"Categoria": "Contas",
			"Preco": 50.00
		}
	]
ˋˋˋ
</br>
As informações acimas são obrigatórias, e é opcional o <strong>"Descricao": "string"</strong>

No envio está configurado para Form Data, File.
