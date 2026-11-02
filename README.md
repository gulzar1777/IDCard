IDCard — Azərbaycan Şəxsiyyət Vəsiqəsi

VISUAL STUDIO-DA AÇMAQ
1. ZIP-i tam çıxart (Extract All). ZIP-in içindən işlətmə.
2. IDCard qovluğundakı IDCard.sln faylını Visual Studio ilə aç.
3. Build > Build Solution seç (Ctrl+Shift+B).
4. Form1.cs üzərinə sağ klik > View Designer (Shift+F7).
5. Proqramı işə salmaq üçün F5 bas.

Lazımdır: Windows, Visual Studio 2019/2022, ".NET desktop development"
komponenti və .NET Framework 4.8 targeting pack.
Bunları Visual Studio Installer > Modify bölməsindən əlavə edə bilərsən.
Bu Windows Forms layihəsi VS Code və macOS-da birbaşa işləmir.

VISUAL STUDIO OLMADAN İŞƏ SALMAQ
Windows-da .NET Framework 4.8 varsa, bin\Release\IDCard.exe faylını aç.
Hazır proqram paketə daxil edilib.
Kodu dəyişdikdən sonra yenidən yığmaq üçün Ise_sal.bat faylını aç;
bu fayl C# kodunu kompilyasiya edir və proqramı işə salır.

İSTİFADƏ
- Sağdakı xanaları doldur: məlumatlar soldakı vəsiqədə dərhal görünür.
- Doğum tarixinin yanındakı işarəni aktiv et və tarixi seç.
- "Şəkil yüklə...": JPG, JPEG, PNG və BMP şəkilləri qəbul edilir.
- "Təmizlə": bütün məlumatları, tarixi və şəkli sıfırlayır.
- "Yadda saxla": PNG olaraq vəsiqəni, yaxud TXT olaraq məlumatları saxlayır.
  TXT faylına foto daxil edilmir. Foto seçimi məcburi deyil.
- Seriya, FİN, verilmə və etibarlılıq tarixləri sərbəst mətn kimi daxil edilir.
  Bu sahələrə format yoxlaması tətbiq edilmir.

FAYLLAR
Form1.cs — düymələr, sahələr, foto seçimi və yadda saxlama.
Form1.Designer.cs — formanın və bütün elementlərin dizaynı.
Form1.resx — forma resursu.
PreviewControls.cs — vəsiqənin çərçivəsi və altı xətli yazılar.
Program.cs — proqramın başlanğıcı.
IDCard.csproj / IDCard.sln — Visual Studio layihəsi.
Properties\AssemblyInfo.cs və App.config — proqram məlumatları.
