# SquidOps Asset Manager

![.NET 8.0](https://img.shields.io/badge/.NET-8.0-Informational?logo=.net\&logoColor=white)
![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen)

A modern, WPF-based desktop application to organize IT assets, generate reports, and print QR code labels via Brother PT-P710BT.

---

## 📑 Table of Contents

* [Overview](#overview)
* [Features](#features)
* [Getting Started](#getting-started)

  * [Prerequisites](#prerequisites)
  * [Clone & Run](#clone--run)
* [Usage](#usage)
* [Contributing](#contributing)
* [TODO](#todo)
* [License](#license)

---

## 📝 Overview

SquidOps Asset Manager provides:

* Centralized **Device** & **Location** CRUD operations
* Filterable, printable **Reports** with DataGrid
* Automatic **QR Code** generation and label printing
* Local **SQLite** storage with **Backup & Restore**
* Persistent window layout across views
* Consistent, global **Menu Bar** for common tasks

Designed for small IT teams to streamline asset tracking and labeling workflows.

---

## 🚀 Features

* **Device Management**: Add, edit, and remove devices with detailed properties
* **Location Management**: Inline location creation and filtering
* **Reporting**: Build & print customizable device reports
* **QR Label Printing**: Generate scannable QR labels on a PT-P710BT
* **Data Persistence**: SQLite database (`inventory.db`) in-app
* **Backup & Restore**: One-click database export/import
* **Window State**: Remember last size, position, and monitor
* **Unified Menu**: File ▶ Backup/Restore/Print/Exit; Settings; Help

---

## 🛠 Getting Started

### Prerequisites

* **Windows 10/11 (x64)**
* **.NET 8.0 Runtime** (or SDK for development)
* **Brother P-Touch Editor 6 SDK** (for label printing COM interop)
* **Visual Studio 2022+** (optional, for source development)

### Clone & Run

```bash
# Clone the repository
git clone https://github.com/YourCompany/SquidOps_AssetSquid.git
cd SquidOps_AssetSquid

# Restore, build, and run
dotnet restore
dotnet build
dotnet run --project SquidOps_AssetSquid.csproj
```

Alternatively, publish a release build and run the standalone EXE:

```bash
dotnet publish -c Release -r win-x64 --self-contained false -o publish
cd publish
./SquidOps_AssetSquid.exe
```

---

## 💡 Usage

1. **Launch** the app and use the top **File** menu for Backup, Restore, and Print (in Reports view).
2. **Navigate** between Devices, Locations, Reports, and Settings via the unified menu bar.
3. **Manage** devices and locations, generate reports, and print QR labels directly to your Brother PT-P710BT.

---

## 🤝 Contributing

We welcome contributions! To get started:

1. **Fork** the repository on GitHub
2. **Clone** your fork and create a feature branch
3. **Commit** your changes with clear messages
4. **Push** and open a **Pull Request** against `main`

Please include tests and update documentation as needed.

---

## ✏️ TODO

* Implement **Preferences** dialog for user settings
* Add **CSV import/export** for bulk data
* Create an **installer** (MSIX/InnoSetup)
* Develop **unit & integration tests**
* Enhance **logging** and error reporting
* Support **localization** & accessibility improvements

---

## 📄 License

This project is licensed under the **MIT License**. See [LICENSE.md](LICENSE.md) for details.

---

*© 2025 YourCompany*

