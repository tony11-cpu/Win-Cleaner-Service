# Win-Cleaner-Service 🧹
**Automated Windows Service for Comprehensive System Junk Cleanup**

## Overview
Win-Cleaner-Service is a robust Windows Service developed in C# (.NET) that proactively eliminates temporary files, browser caches, and accumulated system junk. Designed for operational environments where reliability and automation are essential, it optimizes disk usage and preserves system performance with zero user interaction. The service is scheduled for weekly execution via Task Scheduler, ensuring regular maintenance without manual oversight.

## Features ✨
- **Automated, Unattended Cleaning:** Runs as a Windows Service, operating in the background without user intervention.
- **Broad Junk File Coverage:** Targets temporary files, browser caches, and general system clutter.
- **Scheduled Execution:** Integrates with Windows Task Scheduler for reliable, periodic operation. ⏰
- **Low Resource Footprint:** Engineered for minimal memory and CPU usage, suitable for continuous production deployment.
- **Extensible Design:** Modular codebase allows straightforward adaptation to new file types or cleanup routines.

## Tech Stack ⚙️
- **Language:** C# (.NET)
- **Platform:** Windows Service
- **Task Orchestration:** Windows Task Scheduler

## Architecture & Design Decisions 🏗️
- **Service-Based Model:** Implements as a system Windows Service for deep integration and background operation, supporting secure and continuous task execution even when no user is logged in.
- **Separation of Concerns:** Cleanup logic is isolated from scheduling and service orchestration, improving maintainability and testability.
- **Fail-Safe Cleanup:** Defensive error handling ensures that cleanup is resilient to file locks, permissions, and edge cases without service crashes.
- **Silent Operation:** No UI dependencies; all operations are logged or handled in the background to avoid user disruption.
- **Configurable Scheduling:** Leverages Windows-native Task Scheduler to decouple time-based triggers from service lifecycles, allowing flexible deployment.

## How It Works (High-Level Flow) 🔄
1. **Service Initialization:** On machine boot, the service starts automatically with system privileges.
2. **Trigger:** Task Scheduler invokes the cleanup routine on a weekly cadence, or as configured.
3. **Cleanup Execution:** The service scans known temp directories, browser cache locations, and defined junk paths, methodically deleting eligible files.
4. **Error Handling:** Logs and gracefully skips protected or in-use files to avoid system conflicts.
5. **Completion:** Detailed logs are optionally maintained for audits or diagnostics.

## Setup & Installation 🛠️
> **Prerequisites:** .NET runtime installation and administrator privileges.

1. **Build and Install:**  
   - Build the solution in Release mode.
   - Register the Windows Service using `sc create` or `InstallUtil.exe` (see `docs/` for scripts if included).

2. **Schedule via Task Scheduler:**  
   - Import or create a scheduled task to invoke the service's cleanup executable on your preferred schedule (default: weekly).

3. **Verify Operation:**  
   - Check Windows Services Manager for the running `Win-Cleaner-Service`.
   - Optionally, review logs in the service’s log directory.

## Usage Example 🚀
The service is intended to be background and zero-touch. To trigger ad hoc cleanup:
```powershell
net start Win-Cleaner-Service
```
or use Task Scheduler to run the configured cleanup task on demand.

## Future Improvements 📈
- Optional notification integration (Email/Windows Event Log) for summary reports.
- Custom path inclusion/exclusion rules via an external config file.
- GUI dashboard for service health and cleanup statistics.

---

Serious about efficiency and system hygiene, `Win-Cleaner-Service` prioritizes maintainable code, automation, and operational reliability—ideally suited for both managed enterprise environments and personal power users.