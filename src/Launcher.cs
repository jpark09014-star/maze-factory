// 베타아빠 미로공장 실행기
// 프로그램 안에 들어 있는 미로공장 화면(index.html)을 사용자 폴더에 풀고,
// Microsoft Edge(없으면 Chrome, 그것도 없으면 기본 브라우저)의 앱 창으로 엽니다.
// 인터넷 연결, 설치, 관리자 권한이 필요 없고 개인정보를 수집하거나 전송하지 않습니다.
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

[assembly: AssemblyTitle("베타아빠 미로공장")]
[assembly: AssemblyProduct("베타아빠 미로공장")]
[assembly: AssemblyDescription("문제·정답 미로 도안 만들기 (가정·교육용 무료)")]
[assembly: AssemblyCompany("베타아빠 (blog.naver.com/geekpapa)")]
[assembly: AssemblyCopyright("베타아빠")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

static class Program
{
    [STAThread]
    static void Main()
    {
        try
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BetaDadMazeFactory");
            Directory.CreateDirectory(dir);
            string html = Path.Combine(dir, "index.html");
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("index.html"))
            using (FileStream f = File.Create(html)) { s.CopyTo(f); }

            string url = new Uri(html).AbsoluteUri;
            string profile = Path.Combine(dir, "browser");
            string args = "--app=\"" + url + "\" --window-size=1300,920 --user-data-dir=\"" + profile + "\" --no-first-run --no-default-browser-check";

            string[] candidates = {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Microsoft\Edge\Application\msedge.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Microsoft\Edge\Application\msedge.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Google\Chrome\Application\chrome.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Google\Chrome\Application\chrome.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\Application\chrome.exe")
            };
            foreach (string exe in candidates)
            {
                if (File.Exists(exe))
                {
                    Process.Start(new ProcessStartInfo(exe, args) { UseShellExecute = false });
                    return;
                }
            }
            Process.Start(new ProcessStartInfo(html) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MessageBox.Show("미로공장을 여는 중에 문제가 생겼어요.\n\n" + ex.Message, "베타아빠 미로공장", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
