# Asset Squid

Asset Squid is a modern, WPF-based IT asset management desktop application written in C#. It provides a flexible, scalable solution to track hardware, software, locations, and lifecycles of company assets—complete with QR-code tagging, customizable alerts, and full audit trails.

---

## Table of Contents

* [Features](#features)
* [Requirements](#requirements)
* [Getting Started](#getting-started)

  * [Clone the Repository](#clone-the-repository)
  * [Install Dependencies](#install-dependencies)
  * [Configure Application](#configure-application)
  * [Build & Run](#build--run)
* [Usage](#usage)
* [Configuration](#configuration)
* [Extending the API](#extending-the-api)
* [Contributing](#contributing)
* [License](#license)

---

## Features

* **Centralized Asset Repository**: Add, view, and manage all hardware, software, and peripherals in one place.
* **QR-Code Labeling**: Generate and print QR-code tags for assets for fast scanning.
* **Assignment & Checkout**: Assign assets to users or departments, record history, and set due dates.
* **Maintenance & Warranty Alerts**: Schedule notifications for upcoming maintenance windows and warranty expirations.
* **Custom Fields**: Define locations, device types, departments, and more via dropdown selectors.
* **Full Audit Trail**: Log every change with timestamps and user details.
* **Reporting**: Generate on-demand reports and export data to CSV or PDF.

## Requirements

* Windows 10 or later
* Visual Studio 2022 (or newer) with .NET Desktop Development workload
* .NET 6.0 SDK (or newer)
* SQLite database engine (bundled via NuGet package)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/YourOrg/AssetSquid.git
cd AssetSquid
```

### Install Dependencies

* Restore NuGet packages:

  1. Open the solution in Visual Studio.
  2. Right-click the solution in Solution Explorer and select **Restore NuGet Packages**.

* Required packages:

  * [Dapper](https://www.nuget.org/packages/Dapper)
  * [Microsoft.Data.Sqlite](https://www.nuget.org/packages/Microsoft.Data.Sqlite)
  * QR-code generation/printing library of your choice

### Configure Application

1. Copy `appsettings.example.json` to `appsettings.json`.
2. Open `appsettings.json` and update the `ConnectionString` under `Database`:

   ```json
   "Database": {
     "ConnectionString": "Data Source=assets.db;"
   }
   ```
3. (Optional) Create `appsettings.local.json` for developer-specific overrides. This file is ignored by Git.

### Build & Run

1. In Visual Studio, set **AssetSquid** as the startup project.
2. Press **F5** or click **Debug > Start Debugging**.
3. The WPF client window will launch—log in with your credentials and get started!

## Usage

* **Add Asset**: Click **New Asset**, fill in details, print QR tag, and save.
* **Search & Filter**: Use the search bar or dropdown filters (location, type, department).
* **Check-Out / Check-In**: Select an asset and click **Check-Out**; enter user and due date.
* **Alerts**: View upcoming maintenance and warranty alerts in the **Notifications** panel.
* **Reports**: Go to **Reports** menu to export data.

## Configuration

| Setting                     | Description                      | Location           |
| --------------------------- | -------------------------------- | ------------------ |
| `Database:ConnectionString` | Path to SQLite database file     | `appsettings.json` |
| `Alerts:ReminderDays`       | Days before expiration to notify | `appsettings.json` |
| `QRCode:OutputFolder`       | Folder for generated QR images   | `appsettings.json` |

## Extending the API

The backend exposes a RESTful API for external integration:

* **GET** `/api/assets` – List all assets
* **POST** `/api/assets` – Create new asset
* **PUT** `/api/assets/{id}` – Update asset
* **DELETE** `/api/assets/{id}` – Remove asset

Refer to [API\_DOCUMENTATION.md](docs/API_DOCUMENTATION.md) for full details.

## Contributing

1. Fork the repository.
2. Create a feature branch: `git checkout -b feature/MyNewFeature`
3. Commit your changes: `git commit -m 'Add new feature'`
4. Push to the branch: `git push origin feature/MyNewFeature`
5. Open a Pull Request.

Please follow the existing code style and include unit tests where applicable.

## License

This project is licensed under the [MIT License](LICENSE).
