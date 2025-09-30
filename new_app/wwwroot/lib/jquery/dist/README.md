# jQuery v3.3.1 Library Files

## Migration Status: ⏳ Pending File Copy

### Overview
This directory should contain the jQuery v3.3.1 library files copied from the legacy application. Due to the large file sizes (jquery.js is ~280KB), these files require a file system copy operation.

## Quick Start

### Option 1: Automated Script (Recommended)

**Linux/Mac:**
```bash
chmod +x scripts/copy-jquery-files.sh
./scripts/copy-jquery-files.sh
```

**Windows (PowerShell):**
```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
.\scripts\Copy-JQueryFiles.ps1
```

### Option 2: Manual Copy

```bash
# From repository root
mkdir -p new_app/wwwroot/lib/jquery/dist
cp wwwroot/lib/jquery/dist/jquery.js new_app/wwwroot/lib/jquery/dist/
cp wwwroot/lib/jquery/dist/jquery.min.js new_app/wwwroot/lib/jquery/dist/
cp wwwroot/lib/jquery/dist/jquery.min.map new_app/wwwroot/lib/jquery/dist/
```

### Option 3: Using Git

```bash
cd new_app/wwwroot/lib/jquery/dist
git show HEAD~:wwwroot/lib/jquery/dist/jquery.js > jquery.js
git show HEAD~:wwwroot/lib/jquery/dist/jquery.min.js > jquery.min.js
git show HEAD~:wwwroot/lib/jquery/dist/jquery.min.map > jquery.min.map
```

## Required Files

| File | Size | Purpose |
|------|------|---------|
| **jquery.js** | ~280KB | Unminified version for development |
| **jquery.min.js** | ~85KB | Minified version for production |
| **jquery.min.map** | ~140KB | Source map for debugging |

## Why Copy Without Modification?

✅ **Framework-Agnostic**: jQuery is a client-side JavaScript library  
✅ **.NET 8 Compatible**: Works identically across all .NET versions  
✅ **Third-Party Library**: No customization needed  
✅ **Stable Version**: jQuery v3.3.1 is a tested, stable release  

## Verification

After copying, verify the files:

```bash
# Check all files exist
ls -lh new_app/wwwroot/lib/jquery/dist/

# Verify jQuery version
head -n 5 new_app/wwwroot/lib/jquery/dist/jquery.js
```

Expected output should include:
```
/*!
 * jQuery JavaScript Library v3.3.1
 * https://jquery.com/
 */
```

## Integration with .NET 8

These files are referenced in:
- `Views/Shared/_Layout.cshtml`
- `Views/Shared/_ValidationScriptsPartial.cshtml`

The application serves these via:
```csharp
app.UseStaticFiles(); // in Program.cs
```

## Library Details

### jQuery v3.3.1
- **Release Date**: January 20, 2018
- **License**: MIT
- **Homepage**: https://jquery.com/
- **Documentation**: https://api.jquery.com/

### Features
- DOM manipulation
- Event handling
- AJAX requests
- Animations
- Utility functions

### Browser Support
- Chrome 29+
- Firefox 28+
- Internet Explorer 9+
- Edge 12+
- Safari 6.1+
- Opera 16+

## Troubleshooting

### Issue: Files not found
**Solution**: Ensure the source files exist in `wwwroot/lib/jquery/dist/`

### Issue: Permission denied
**Solution**: On Linux/Mac, run `chmod +x scripts/copy-jquery-files.sh`

### Issue: Size mismatch
**Solution**: Re-copy the files or verify source file integrity

## Next Steps

After copying these files:
1. ✅ Copy jquery.js (280KB)
2. ✅ Copy jquery.min.js (85KB)
3. ✅ Copy jquery.min.map (140KB)
4. ✅ Verify file sizes match
5. ✅ Test application loads jQuery correctly
6. ✅ Commit files to git

## Related Files

This directory is part of the jQuery library migration. Also need to copy:
- `wwwroot/lib/jquery-validation/` - jQuery validation plugin
- `wwwroot/lib/jquery-validation-unobtrusive/` - Unobtrusive validation

## Support

For jQuery-related issues:
- **jQuery Documentation**: https://api.jquery.com/
- **jQuery GitHub**: https://github.com/jquery/jquery
- **Stack Overflow**: Tag with `jquery`

For migration issues:
- Refer to migration plan documentation
- Check `.migration-note.md` in parent directory

---

**Last Updated**: .NET 8 Migration  
**Status**: Awaiting file copy operation  
**Priority**: High - Required for application functionality
