using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NetCoreProjectsList.Componenets.AppSettings;
using NetCoreProjectsList.Componenets.Paging;
using NetCoreProjectsList.Models;
using NetCoreProjectsList.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreProjectsList.Controllers
{
    public class ProjectsController : BaseController
    {
        private static readonly string NOT_FOUND_ERROR = "Project not found, try again";

        private IProjectsRepository projectsRepo;
        private AppSettings appSettings;

        public ProjectsController(IProjectsRepository projectsRepo, IOptions<AppSettings> appSettings)
        {
            this.projectsRepo = projectsRepo;
            this.appSettings = appSettings.Value;
        }

        public IActionResult Index(string nameFilter, int pageNo)
        {
            var pagingInfo = GetPagingInfo(pageNo);
            PagedList<Project> projects = projectsRepo.GetAll(nameFilter, pagingInfo);
            ViewBag.NameFilter = nameFilter;
            return View(nameof(Index), projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id, string nameFilter, int pageNo)
        {
            var project = projectsRepo.Get(id);
            if (project == null)
            {
                return IndexWithError(NOT_FOUND_ERROR, nameFilter, pageNo);
            }
            return View(nameof(ProjectsController.Create), project);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                projectsRepo.Add(project);
                return RedirectToAction(nameof(Details), new { id = project.ProjectId });
            }
            return View(project);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                projectsRepo.Edit(project);
                return RedirectToAction(nameof(Details), new { id = project.ProjectId });
            }
            return View(nameof(Create), project);
        }

        public IActionResult Delete(int id, string nameFilter, int pageNo)
        {
            var project = projectsRepo.Get(id);
            if(project == null)
            {
                return IndexWithError(NOT_FOUND_ERROR, nameFilter, pageNo);
            }
            projectsRepo.Delete(id);
            return IndexRedirect(nameFilter, pageNo);
        }

        public IActionResult Details(int id, string nameFilter, int pageNo)
        {
            var project = projectsRepo.SingleOrDefaultWithTasks(id);
            if(project == null)
            {
                return IndexWithError(NOT_FOUND_ERROR, nameFilter, pageNo);
            }
            return View(project);
        }

        private PagingInfo GetPagingInfo(int pageNo)
        {
            return new PagingInfo()
            {
                CurrentPage = pageNo,
                ItemsPerPage = appSettings.ItemsPerPage
            };
        }

        private IActionResult IndexWithError(string errorMessage, string nameFilter, int pageNo)
        {
            AddTemporaryError(errorMessage);
            return IndexRedirect(nameFilter, pageNo);
        }

        private IActionResult IndexRedirect(string nameFilter, int pageNo)
        {
            return RedirectToAction(nameof(Index), new { nameFilter = nameFilter, pageNo = pageNo });
        }
    }
}