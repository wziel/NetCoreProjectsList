using Microsoft.AspNetCore.Mvc;
using NetCoreProjectsList.Models;
using NetCoreProjectsList.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreProjectsList.Controllers
{
    public class TasksController : BaseController
    {
        private static readonly string NOT_FOUND_ERROR = "Task not found, try again";

        private ITasksRepository tasksRepo;
        private IProjectsRepository projectsRepo;

        public TasksController(ITasksRepository tasksRepo, IProjectsRepository projectsRepo)
        {
            this.tasksRepo = tasksRepo;
            this.projectsRepo = projectsRepo;
        }

        public IActionResult Create(int projectId)
        {
            var project = projectsRepo.Get(projectId);
            if (project == null)
            {
                return ProjectDetailsWithError(projectId, NOT_FOUND_ERROR);
            }
            var task = new Task()
            {
                ProjectId = project.ProjectId,
                Project = project
            };
            return View(task);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                task.Project = null;
                tasksRepo.Add(task);
                return TaskDetails(task);
            }
            return View(task);
        }

        public IActionResult Details(int id, int projectId)
        {
            var task = tasksRepo.GetWithProject(id);
            if (task == null)
            {
                return ProjectDetailsWithError(projectId, NOT_FOUND_ERROR);
            }
            return View(task);
        }

        public IActionResult Edit(int id, int projectId)
        {
            var task = tasksRepo.GetWithProject(id);
            if (task == null)
            {
                return ProjectDetailsWithError(projectId, NOT_FOUND_ERROR);
            }
            return View(nameof(Create), task);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                task.Project = null;
                tasksRepo.Edit(task);
                return TaskDetails(task);
            }
            return View(nameof(Create), task);
        }

        public IActionResult Delete(int id, int projectId)
        {
            if (tasksRepo.Get(id) == null)
            {
                return ProjectDetailsWithError(projectId, NOT_FOUND_ERROR);
            }
            tasksRepo.Delete(id);
            return ProjectDetails(projectId);
        }

        private IActionResult TaskDetails(Task task)
        {
            return RedirectToAction(nameof(Details), new { id = task.TaskId, projectId = task.ProjectId });
        }

        private IActionResult ProjectDetailsWithError(int projectId, string error)
        {
            AddTemporaryError(error);
            return ProjectDetails(projectId);
        }

        private IActionResult ProjectDetails(int projectId)
        {
            return RedirectToAction(nameof(ProjectsController.Details), "Projects", new { id = projectId });
        }
    }
}