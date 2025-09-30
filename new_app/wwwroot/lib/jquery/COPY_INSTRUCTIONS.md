# jQuery Library Files - Copy Instructions

## Overview
This directory should contain the jQuery v3.3.1 library files copied from the legacy application.

## Required Actions

### Manual File Copy Required
Due to the large size of the jQuery library files (jquery.js is ~280KB), these files need to be copied using file system operations or Git commands.

### Files to Copy

Execute the following commands from the repository root:

```bash
# Create the target directory structure
mkdir -p new_app/wwwroot/lib/jquery/dist

# Copy all jQuery dist files
cp wwwroot/lib/jquery/dist/jquery.js new_app/wwwroot/lib/jquery/dist/jquery.js
cp wwwroot/lib/jquery/dist/jquery.min.js new_app/wwwroot/lib/jquery/dist/jquery.min.js
cp wwwroot/lib/jquery/dist/jquery.min.map new_app/wwwroot/lib/jquery/dist/jquery.min.map
```

### Alternative: Using Git Commands

```bash
# From repository root
git checkout net-migration-v2

# Copy jQuery files
git show HEAD:wwwroot/lib/jquery/dist/jquery.js > new_app/wwwroot/lib/jquery/dist/jquery.js
git show HEAD:wwwroot/lib/jquery/dist/jquery.min.js > new_app/wwwroot/lib/jquery/dist/jquery.min.js
git show HEAD:wwwroot/lib/jquery/dist/jquery.min.map > new_app/wwwroot/lib/jquery/dist/jquery.min.map

# Add to git
git add new_app/wwwroot/lib/jquery/dist/
git commit -m "Copy jQuery v3.3.1 library files to new_app"
```

### Files List

| File | Size | Description |
|------|------|-------------|
| jquery.js | ~280KB | Unminified development version |
| jquery.min.js | ~85KB | Minified production version |
| jquery.min.map | ~140KB | Source map for debugging |

### Verification

After copying, verify the files:

```bash
# Check files exist
ls -lh new_app/wwwroot/lib/jquery/dist/

# Verify jQuery version in the file
head -n 15 new_app/wwwroot/lib/jquery/dist/jquery.js | grep "v3.3.1"

# Should output: * jQuery JavaScript Library v3.3.1
```

### Expected Output

```
total 504K
-rw-r--r-- 1 user group 281K date jquery.js
-rw-r--r-- 1 user group  85K date jquery.min.js
-rw-r--r-- 1 user group 138K date jquery.min.map
```

## Why These Files Are Not Modified

- ✅ jQuery is a **third-party JavaScript library**
- ✅ It is **framework-agnostic** and **platform-independent**
- ✅ No changes are needed for .NET 8 compatibility
- ✅ The files work identically in .NET Framework, .NET Core, and .NET 8
- ✅ These are client-side assets served as static files

## Integration with .NET 8 Application

These files are referenced in:
- **Views/Shared/_Layout.cshtml** - Main layout template
- **Views/Shared/_ValidationScriptsPartial.cshtml** - Validation scripts

The .NET 8 application will serve these files via the static files middleware configured in `Program.cs`:

```csharp
app.UseStaticFiles(); // Serves files from wwwroot/
```

## Completion Checklist

- [ ] jQuery files copied to new_app/wwwroot/lib/jquery/dist/
- [ ] File sizes match source files
- [ ] jQuery version verified as 3.3.1
- [ ] Files committed to git repository
- [ ] Application tested with jQuery functionality

## Next Steps

After copying these files:
1. Continue with other jQuery-related files (jquery-validation, jquery-validation-unobtrusive)
2. Test the application to ensure jQuery loads correctly
3. Verify client-side validation works
4. Check browser console for any JavaScript errors

---

**Status**: ⏳ Manual copy required  
**Priority**: High - Required for application functionality  
**Estimated Time**: 1 minute
