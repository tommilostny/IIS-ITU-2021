# IIS/ITU projekt 2021

Dokumentace: https://docs.google.com/document/d/1LOPLomIcY20bSUCo_LaWtql6IJ3Jzo4FaAkDg5XLb7s/edit?usp=sharing

Softwarové požadavky:
* SDK: .NET 6.0.100
* Runtime: Microsoft.AspNetCore.App 6.0.0, Microsoft.NETCore.App 6.0.0
* Visual Studio 2022

Postup spuštění je Extrahovat archiv, otevřít soubor **Fituska/Fituska.sln** ve Visual Studiu.

1. Pravým tlačítkem kliknout na Solution v sekci **Solution Explorer**
2. Vybrat **Set Startup Projects…**
3. Vybrat **Multiple Projects** a nastavit **Start** pro projekty **Fituska.API** a **Fituska.Client**.
4. V režimu Debug dojde ke spuštění s lokální Sqlite databází, pro Release na Microsoft Azure je použita Azure SQL Server. **Spouštět pouze v režimu Debug**!
