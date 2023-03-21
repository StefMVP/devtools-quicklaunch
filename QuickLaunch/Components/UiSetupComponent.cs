using AppQuickLaunch.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppQuickLaunch
{
    public class UiSetupComponent : IUiSetupComponent
    {
        private readonly QuickLaunchConfig _config;
        private readonly IUiEventHandlers _uiEventHandlers;

        public UiSetupComponent(IConfigCache cache, IUiEventHandlers uiEventHandlers)
        {
            _config = cache.GetConfig();
            _uiEventHandlers = uiEventHandlers;
        }

        private void SetupSvcControls(List<AppApplicationService> apps)
        {
            const int yPosInit = 0;
            int yPos = yPosInit;
            const int rowPadding = 25;
            const int secondRowYPadding = 25;
            const int thirdRowYPadding = secondRowYPadding + 25;
            const int baseMarginLeft = 20;
            const int btnWidth = 75;
            const int yPosInc = 100;
            const int ySettingsPosInc = 50;
            const int statusTxtHeight = 20;
            const int statustxtWidth = 30;
            const int xPosInc = 80;
            const int nameLblMarginLeft = -5;
            const int btnMarginLeftIncrement = 50;
            var grid = new Grid();
            apps.ForEach(app =>
            {
                var currMarginLeft = baseMarginLeft;
                //StatusTxt
                var statusTxtMargin = new FrameworkElement().Margin;
                statusTxtMargin.Left = currMarginLeft;
                statusTxtMargin.Top = yPos + rowPadding;
                var statusTxt = new TextBox
                {
                    Name = app.StatusTxtName,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Height = statusTxtHeight,
                    Margin = statusTxtMargin,
                    Background = Brushes.Red,
                    Width = statustxtWidth,
                    IsEnabled = false
                };
                currMarginLeft += btnMarginLeftIncrement;

                //Namelbl
                var namelblMargin = new FrameworkElement().Margin;
                namelblMargin.Left = currMarginLeft + nameLblMarginLeft;
                namelblMargin.Top = yPos;
                var namelbl = new Label
                {
                    Name = app.LabelName,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = $"{app.DisplayName} - Branch: N/A",
                    Margin = namelblMargin,
                    FontWeight = FontWeight.FromOpenTypeWeight(500)
                };

                //Run Btn
                var runBtnMargin = new FrameworkElement().Margin;
                runBtnMargin.Left = currMarginLeft;
                runBtnMargin.Top = yPos + rowPadding;
                var runBtn = new Button
                {
                    Name = app.RunBtnName,
                    Content = "Run",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = runBtnMargin
                };
                runBtn.Click += _uiEventHandlers.RunAppClick;

                currMarginLeft += xPosInc;

                //Kill Btn
                var killBtnMargin = new FrameworkElement().Margin;
                killBtnMargin.Left = currMarginLeft;
                killBtnMargin.Top = yPos + rowPadding;
                var killBtn = new Button
                {
                    Name = app.KillBtnName,
                    Content = "Kill",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = killBtnMargin
                };
                killBtn.Click += _uiEventHandlers.KillAppClick;

                currMarginLeft += xPosInc;

                //Test Btn
                var testBtnMargin = new FrameworkElement().Margin;
                testBtnMargin.Left = currMarginLeft;
                testBtnMargin.Top = yPos + rowPadding;
                var testBtn = new Button
                {
                    Name = app.TestBtnName,
                    Content = "Test",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = testBtnMargin
                };
                testBtn.Click += _uiEventHandlers.RunAppClick;
                grid.Children.Add(testBtn);

                currMarginLeft += xPosInc;

                //Solution Btn
                var slnBtnMargin = new FrameworkElement().Margin;
                slnBtnMargin.Left = currMarginLeft;
                slnBtnMargin.Top = yPos + rowPadding;
                var slnBtn = new Button
                {
                    Name = app.SlnBtnName,
                    Content = "Sln",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = slnBtnMargin
                };
                slnBtn.Click += _uiEventHandlers.OpenSolutionClick;
                if (string.IsNullOrEmpty(app.SolutionPath))
                {
                    slnBtn.IsEnabled = false;
                }
                grid.Children.Add(slnBtn);

                currMarginLeft += xPosInc;

                //Folder Btn
                var folderBtnMargin = new FrameworkElement().Margin;
                folderBtnMargin.Left = currMarginLeft;
                folderBtnMargin.Top = yPos + rowPadding;
                var folderBtn = new Button
                {
                    Name = app.FolderBtnName,
                    Content = "Folder",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = folderBtnMargin
                };
                folderBtn.Click += _uiEventHandlers.OpenFolderClick;
                grid.Children.Add(folderBtn);
                if (string.IsNullOrEmpty(app.ProjectFolder))
                {
                    folderBtn.IsEnabled = false;
                }

                currMarginLeft = baseMarginLeft;

                //Second Row Open Url Local
                var urlLocalBtnMargin = new FrameworkElement().Margin;
                urlLocalBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlLocalBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlLocalBtn = new Button
                {
                    Name = app.OpenUrlLocalBtnName,
                    Content = "LocalUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlLocalBtnMargin
                };
                urlLocalBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppLocalUrl))
                {
                    urlLocalBtn.IsEnabled = false;
                }
                grid.Children.Add(urlLocalBtn);

                //Second Row Open Url Dev
                currMarginLeft += xPosInc;
                var urlDvBtnMargin = new FrameworkElement().Margin;
                urlDvBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlDvBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlDvBtn = new Button
                {
                    Name = app.OpenUrlDvBtnName,
                    Content = "DevUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlDvBtnMargin
                };
                urlDvBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppDvUrl))
                {
                    urlDvBtn.IsEnabled = false;
                }
                grid.Children.Add(urlDvBtn);

                //Second Row Open Url Qa
                currMarginLeft += xPosInc;
                var urlQaBtnMargin = new FrameworkElement().Margin;
                urlQaBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlQaBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlQaBtn = new Button
                {
                    Name = app.OpenUrlQaBtnName,
                    Content = "QaUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlQaBtnMargin
                };
                urlQaBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppQaUrl))
                {
                    urlQaBtn.IsEnabled = false;
                }
                grid.Children.Add(urlQaBtn);

                //Second Row Open Url Sg
                currMarginLeft += xPosInc;
                var urlSgBtnMargin = new FrameworkElement().Margin;
                urlSgBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlSgBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlSgBtn = new Button
                {
                    Name = app.OpenUrlSgBtnName,
                    Content = "StageUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlSgBtnMargin
                };
                urlSgBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppSgUrl))
                {
                    urlSgBtn.IsEnabled = false;
                }
                grid.Children.Add(urlSgBtn);

                //Second Row Open Url Pd
                currMarginLeft += xPosInc;
                var urlPdBtnMargin = new FrameworkElement().Margin;
                urlPdBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlPdBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlPdBtn = new Button
                {
                    Name = app.OpenUrlPdBtnName,
                    Content = "ProdUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlPdBtnMargin
                };
                urlPdBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppPdUrl))
                {
                    urlPdBtn.IsEnabled = false;
                }
                grid.Children.Add(urlPdBtn);

                currMarginLeft = baseMarginLeft;

                //Third Row Open Url (Repo)
                var repoBtnMargin = new FrameworkElement().Margin;
                repoBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                repoBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var repoBtn = new Button
                {
                    Name = app.RepositoryUrlBtnName,
                    Content = "Repo",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = repoBtnMargin
                };
                repoBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.RepositoryUrl))
                {
                    repoBtn.IsEnabled = false;
                }
                grid.Children.Add(repoBtn);

                //Third Row Open Url (Pull Requests)
                currMarginLeft += xPosInc;
                var pullRequestsBtnMargin = new FrameworkElement().Margin;
                pullRequestsBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                pullRequestsBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var pullRequestsBtn = new Button
                {
                    Name = app.PullRequestsUrlBtnName,
                    Content = "PR",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = pullRequestsBtnMargin
                };
                pullRequestsBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.PullRequestsUrl))
                {
                    pullRequestsBtn.IsEnabled = false;
                }
                grid.Children.Add(pullRequestsBtn);

                //Third Row Open Url (Builds)
                currMarginLeft += xPosInc;
                var buildsBtnMargin = new FrameworkElement().Margin;
                buildsBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                buildsBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var buildsBtn = new Button
                {
                    Name = app.BuildsUrlBtnName,
                    Content = "Builds",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = buildsBtnMargin
                };
                buildsBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.BuildsUrl))
                {
                    buildsBtn.IsEnabled = false;
                }
                grid.Children.Add(buildsBtn);

                //Third Row Open Url (Releases)
                currMarginLeft += xPosInc;
                var releasesBtnMargin = new FrameworkElement().Margin;
                releasesBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                releasesBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var releasesBtn = new Button
                {
                    Name = app.ReleasesUrlBtnName,
                    Content = "Releases",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = releasesBtnMargin
                };
                releasesBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.ReleasesUrl))
                {
                    releasesBtn.IsEnabled = false;
                }
                grid.Children.Add(releasesBtn);

                //Third Row Open Url (Clone Git)
                currMarginLeft += xPosInc;
                var gitCloneBtnMargin = new FrameworkElement().Margin;
                gitCloneBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                gitCloneBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var gitCloneBtn = new Button
                {
                    Name = app.GitCloneBtnName,
                    Content = "Git Clone",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = gitCloneBtnMargin
                };
                gitCloneBtn.Click += _uiEventHandlers.GitCloneClick;

                if (!string.IsNullOrEmpty(app.GitFolder) && Directory.Exists($"{_config.SourceDirectory}/{app.GitFolder}"))
                {
                    gitCloneBtn.IsEnabled = false;
                }
                grid.Children.Add(gitCloneBtn);

                grid.Children.Add(namelbl);
                grid.Children.Add(statusTxt);
                grid.Children.Add(runBtn);
                grid.Children.Add(killBtn);
                yPos += yPosInc;

                SetTabContent(grid, app.TabName);
            });
        }

        //TODO: Refactor for Web/Services to share setup code.
        private void SetupWebControls(List<AppApplicationWebApp> apps)
        {
            const int yPosInit = 0;
            int yPos = yPosInit;
            const int rowPadding = 25;
            const int secondRowYPadding = 25;
            const int thirdRowYPadding = secondRowYPadding + 25;
            const int baseMarginLeft = 20;
            const int btnWidth = 75;
            const int yPosInc = 100;
            const int ySettingsPosInc = 50;
            const int statusTxtHeight = 20;
            const int statustxtWidth = 30;
            const int xPosInc = 80;
            const int nameLblMarginLeft = -5;
            const int btnMarginLeftIncrement = 50;
            var grid = new Grid();
            apps.ForEach(app =>
            {
                var currMarginLeft = baseMarginLeft;
                //StatusTxt
                var statusTxtMargin = new FrameworkElement().Margin;
                statusTxtMargin.Left = currMarginLeft;
                statusTxtMargin.Top = yPos + rowPadding;
                var statusTxt = new TextBox
                {
                    Name = app.StatusTxtName,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Height = statusTxtHeight,
                    Margin = statusTxtMargin,
                    Background = Brushes.Red,
                    Width = statustxtWidth,
                    IsEnabled = false
                };
                currMarginLeft += btnMarginLeftIncrement;

                //Namelbl
                var namelblMargin = new FrameworkElement().Margin;
                namelblMargin.Left = currMarginLeft + nameLblMarginLeft;
                namelblMargin.Top = yPos;
                var namelbl = new Label
                {
                    Name = app.LabelName,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = $"{app.DisplayName} - Branch: N/A",
                    Margin = namelblMargin,
                    FontWeight = FontWeight.FromOpenTypeWeight(500)
                };

                //Run Btn
                var runBtnMargin = new FrameworkElement().Margin;
                runBtnMargin.Left = currMarginLeft;
                runBtnMargin.Top = yPos + rowPadding;
                var runBtn = new Button
                {
                    Name = app.RunBtnName,
                    Content = "Run",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = runBtnMargin
                };
                runBtn.Click += _uiEventHandlers.RunAppClick;

                currMarginLeft += xPosInc;

                //Kill Btn
                var killBtnMargin = new FrameworkElement().Margin;
                killBtnMargin.Left = currMarginLeft;
                killBtnMargin.Top = yPos + rowPadding;
                var killBtn = new Button
                {
                    Name = app.KillBtnName,
                    Content = "Kill",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = killBtnMargin
                };

                killBtn.Click += _uiEventHandlers.KillAppClick;

                currMarginLeft += xPosInc;

                //Test Btn
                var testBtnMargin = new FrameworkElement().Margin;
                testBtnMargin.Left = currMarginLeft;
                testBtnMargin.Top = yPos + rowPadding;
                var testBtn = new Button
                {
                    Name = app.TestBtnName,
                    Content = "Test",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = testBtnMargin
                };

                testBtn.Click += _uiEventHandlers.RunAppClick;
                grid.Children.Add(testBtn);

                currMarginLeft += xPosInc;

                //Folder Btn
                var folderBtnMargin = new FrameworkElement().Margin;
                folderBtnMargin.Left = currMarginLeft;
                folderBtnMargin.Top = yPos + rowPadding;
                var folderBtn = new Button
                {
                    Name = app.FolderBtnName,
                    Content = "Folder",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = folderBtnMargin
                };
                folderBtn.Click += _uiEventHandlers.OpenFolderClick;
                grid.Children.Add(folderBtn);
                if (string.IsNullOrEmpty(app.ProjectFolder))
                {
                    folderBtn.IsEnabled = false;
                }

                currMarginLeft = baseMarginLeft;

                //Second Row Open Url Local
                var urlLocalBtnMargin = new FrameworkElement().Margin;
                urlLocalBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlLocalBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlLocalBtn = new Button
                {
                    Name = app.OpenUrlLocalBtnName,
                    Content = "LocalUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlLocalBtnMargin
                };
                urlLocalBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppLocalUrl))
                {
                    urlLocalBtn.IsEnabled = false;
                }
                grid.Children.Add(urlLocalBtn);

                //Second Row Open Url Dev
                currMarginLeft += xPosInc;
                var urlDvBtnMargin = new FrameworkElement().Margin;
                urlDvBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlDvBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlDvBtn = new Button
                {
                    Name = app.OpenUrlDvBtnName,
                    Content = "DevUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlDvBtnMargin
                };
                urlDvBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppDvUrl))
                {
                    urlDvBtn.IsEnabled = false;
                }
                grid.Children.Add(urlDvBtn);

                //Second Row Open Url Qa
                currMarginLeft += xPosInc;
                var urlQaBtnMargin = new FrameworkElement().Margin;
                urlQaBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlQaBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlQaBtn = new Button
                {
                    Name = app.OpenUrlQaBtnName,
                    Content = "QaUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlQaBtnMargin
                };
                urlQaBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppQaUrl))
                {
                    urlQaBtn.IsEnabled = false;
                }
                grid.Children.Add(urlQaBtn);

                //Second Row Open Url Sg
                currMarginLeft += xPosInc;
                var urlSgBtnMargin = new FrameworkElement().Margin;
                urlSgBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlSgBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlSgBtn = new Button
                {
                    Name = app.OpenUrlSgBtnName,
                    Content = "StageUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlSgBtnMargin
                };
                urlSgBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppSgUrl))
                {
                    urlSgBtn.IsEnabled = false;
                }
                grid.Children.Add(urlSgBtn);

                //Second Row Open Url Pd
                currMarginLeft += xPosInc;
                var urlPdBtnMargin = new FrameworkElement().Margin;
                urlPdBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                urlPdBtnMargin.Top = yPos + rowPadding + secondRowYPadding;
                var urlPdBtn = new Button
                {
                    Name = app.OpenUrlPdBtnName,
                    Content = "ProdUrl",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = urlPdBtnMargin
                };
                urlPdBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.AppPdUrl))
                {
                    urlPdBtn.IsEnabled = false;
                }
                grid.Children.Add(urlPdBtn);

                currMarginLeft = baseMarginLeft;

                //Third Row Open Url (Repo)
                var repoBtnMargin = new FrameworkElement().Margin;
                repoBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                repoBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var repoBtn = new Button
                {
                    Name = app.RepositoryUrlBtnName,
                    Content = "Repo",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = repoBtnMargin
                };
                repoBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.RepositoryUrl))
                {
                    repoBtn.IsEnabled = false;
                }
                grid.Children.Add(repoBtn);

                //Third Row Open Url (Pull Requests)
                currMarginLeft += xPosInc;
                var pullRequestsBtnMargin = new FrameworkElement().Margin;
                pullRequestsBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                pullRequestsBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var pullRequestsBtn = new Button
                {
                    Name = app.PullRequestsUrlBtnName,
                    Content = "PR",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = pullRequestsBtnMargin
                };
                pullRequestsBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.PullRequestsUrl))
                {
                    pullRequestsBtn.IsEnabled = false;
                }
                grid.Children.Add(pullRequestsBtn);

                //Third Row Open Url (Builds)
                currMarginLeft += xPosInc;
                var buildsBtnMargin = new FrameworkElement().Margin;
                buildsBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                buildsBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var buildsBtn = new Button
                {
                    Name = app.BuildsUrlBtnName,
                    Content = "Builds",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = buildsBtnMargin
                };
                buildsBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.BuildsUrl))
                {
                    buildsBtn.IsEnabled = false;
                }
                grid.Children.Add(buildsBtn);

                //Third Row Open Url (Releases)
                currMarginLeft += xPosInc;
                var releasesBtnMargin = new FrameworkElement().Margin;
                releasesBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                releasesBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var releasesBtn = new Button
                {
                    Name = app.ReleasesUrlBtnName,
                    Content = "Releases",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = releasesBtnMargin
                };
                releasesBtn.Click += _uiEventHandlers.OpenUrlClick;
                if (string.IsNullOrEmpty(app.ReleasesUrl))
                {
                    releasesBtn.IsEnabled = false;
                }
                grid.Children.Add(releasesBtn);

                //Third Row Open Url (Clone Git)
                currMarginLeft += xPosInc;
                var gitCloneBtnMargin = new FrameworkElement().Margin;
                gitCloneBtnMargin.Left = currMarginLeft + btnMarginLeftIncrement;
                gitCloneBtnMargin.Top = yPos + rowPadding + thirdRowYPadding;
                var gitCloneBtn = new Button
                {
                    Name = app.GitCloneBtnName,
                    Content = "Git Clone",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = btnWidth,
                    Margin = gitCloneBtnMargin
                };
                gitCloneBtn.Click += _uiEventHandlers.GitCloneClick;

                if (!string.IsNullOrEmpty(app.GitFolder) && Directory.Exists($"{_config.SourceDirectory}/{app.GitFolder}"))
                {
                    gitCloneBtn.IsEnabled = false;
                }
                grid.Children.Add(gitCloneBtn);

                grid.Children.Add(namelbl);
                grid.Children.Add(statusTxt);
                grid.Children.Add(runBtn);
                grid.Children.Add(killBtn);
                yPos += yPosInc;

                SetTabContent(grid, app.TabName);
            });
        }

        private void SetupSettingsControls()
        {
            //TODO: Put these inits into a sharable class
            //TODO2: Better refactor setup controls
            const int yPosInit = 0;
            int yPos = yPosInit;
            const int rowPadding = 25;
            const int secondRowYPadding = 25;
            const int thirdRowYPadding = secondRowYPadding + 25;
            const int baseMarginLeft = 20;
            const int btnWidth = 75;
            const int yPosInc = 100;
            const int ySettingsPosInc = 50;
            const int statusTxtHeight = 20;
            const int statustxtWidth = 30;
            const int xPosInc = 80;
            const int nameLblMarginLeft = -5;
            const int btnMarginLeftIncrement = 50;

            var grid = new Grid();
            //Generate Config btn
            var configBtnMargin = new FrameworkElement().Margin;
            configBtnMargin.Left = baseMarginLeft;
            configBtnMargin.Top = yPos + rowPadding;
            var configBtn = new Button
            {
                Name = "GenerateConfigBtn",
                Content = "Create Default Config",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = btnWidth * 2,
                Margin = configBtnMargin
            };
            yPos += ySettingsPosInc;

            //Open Config btn
            var openConfigBtnMargin = new FrameworkElement().Margin;
            openConfigBtnMargin.Left = baseMarginLeft;
            openConfigBtnMargin.Top = yPos + rowPadding;
            var openConfigBtn = new Button
            {
                Name = "OpenConfigBtn",
                Content = "Open Config",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = btnWidth * 2,
                Margin = openConfigBtnMargin
            };
            openConfigBtn.Click += _uiEventHandlers.OpenConfigClick;
            configBtn.Click += _uiEventHandlers.GenerateConfigClick;
            grid.Children.Add(configBtn);
            grid.Children.Add(openConfigBtn);
            var settingsTab = UiUtils.GetTabItemByTabHeader("Settings");
            settingsTab.Content = grid;
        }

        public void SetupControlsForEachType()
        {
            SetupSvcControls(_config.AppApplicationServices.Where(app => app.TabName == TabNameTypes.ServicesCore).ToList());
            SetupWebControls(_config.AppApplicationWebApps.Where(app => app.TabName == TabNameTypes.WebCore).ToList());

            SetupSettingsControls();
        }

        private void SetTabContent(Grid grid, string tabName)
        {
            var scrollViewer = UiUtils.GetScrollViewerByTabHeader(tabName);
            if (scrollViewer != null)
            {
                scrollViewer.Content = grid;
            }
        }

        public void SetupStatusTimer()
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += _uiEventHandlers.OnTimedEvent;
            aTimer.Interval = 3000;
            aTimer.Enabled = true;
        }
    }
}