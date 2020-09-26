# GameNewsWebCrawler
 Este projeto é um web crawler que capta notícias de games do site comboinfinito e guarda os dados adquiridos em um banco de dados SQL Server (EXPRESS), guardando a 1ª pagina de notícias.
 
# Pacotes Nuget
   - HtmlAgilityPack v1.11.24
   - Microsoft.EntityFrameworkCore v3.1.8
   - Microsoft.EntityFrameworkCore.SqlVerver v3.1.8
   - Microsoft.EntityFrameworkCore.Tools v3.1.8
   - System.Net.Http v4.3.4
 
# Notas 
 > A string de conexão deve ser alterada quando executado em outra maquina;
 
 > Necessita ter SQL server e é recomendavel o SSMS para visualizar o banco de dados;
 
 > Executar no console de pacotes os comandos "Add-migrations initialMigration" e "update-database" para criar o banco;
 
 > Link utilizado: [Combo Infinito](https://www.comboinfinito.com.br/principal/category/games/);
