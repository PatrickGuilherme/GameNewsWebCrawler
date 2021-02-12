# WEB CRAWLER GAMER
Este projeto é um web crawler que capta notícias de games do site comboinfinito e guarda os dados adquiridos em um banco de dados SQL Server (EXPRESS), guardando a 1ª pagina de notícias.
 
 
# PRÉ-REQUISITOS E INSTALAÇÃO
Para execultar a aplicação é necessário que se tenha o visual studio 2019 ou posterior com os pacotes Universal Windows Plataform Development e .NET Desktop development. Apos abrir o jogo através do visual studio deve-se execultar a solução, com isso será iniciado o jogo em uma nova janela de console.

* Pacotes nuget utilizados
   - HtmlAgilityPack v1.11.24
   - Microsoft.EntityFrameworkCore v3.1.8
   - Microsoft.EntityFrameworkCore.SqlVerver v3.1.8
   - Microsoft.EntityFrameworkCore.Tools v3.1.8
   - System.Net.Http v4.3.4
 
# OBSERVAÇÕES SOBRE O CÓDIGO 
 * A string de conexão deve ser alterada quando executado em outra maquina;
 
 * Necessita ter SQL server e é recomendavel o SSMS para visualizar o banco de dados;
 
 * Executar no console de pacotes os comandos "Add-migrations initialMigration" e "update-database" para criar o banco;
 
 * Link utilizado: [Combo Infinito](https://www.comboinfinito.com.br/principal/category/games/);
