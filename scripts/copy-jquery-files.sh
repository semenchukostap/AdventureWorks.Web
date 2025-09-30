#!/bin/bash

##############################################################################
# jQuery Library Files Copy Script
# Purpose: Copy jQuery v3.3.1 library files from legacy app to new .NET 8 app
# Usage: ./scripts/copy-jquery-files.sh
##############################################################################

set -e  # Exit on error

# Colors for output
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

# Script directory
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
REPO_ROOT="$( cd "$SCRIPT_DIR/.." && pwd )"

echo -e "${GREEN}========================================${NC}"
echo -e "${GREEN}jQuery v3.3.1 Migration Script${NC}"
echo -e "${GREEN}========================================${NC}"
echo ""

# Source and destination paths
SOURCE_DIR="$REPO_ROOT/wwwroot/lib/jquery/dist"
DEST_DIR="$REPO_ROOT/new_app/wwwroot/lib/jquery/dist"

echo -e "${YELLOW}Source:${NC} $SOURCE_DIR"
echo -e "${YELLOW}Destination:${NC} $DEST_DIR"
echo ""

# Check if source directory exists
if [ ! -d "$SOURCE_DIR" ]; then
    echo -e "${RED}Error: Source directory does not exist: $SOURCE_DIR${NC}"
    exit 1
fi

# Create destination directory
echo -e "${YELLOW}Creating destination directory...${NC}"
mkdir -p "$DEST_DIR"

# Files to copy
FILES=(
    "jquery.js"
    "jquery.min.js"
    "jquery.min.map"
)

# Copy each file
echo -e "${YELLOW}Copying jQuery library files...${NC}"
echo ""

for file in "${FILES[@]}"; do
    SOURCE_FILE="$SOURCE_DIR/$file"
    DEST_FILE="$DEST_DIR/$file"
    
    if [ -f "$SOURCE_FILE" ]; then
        echo -e "  ➜ Copying ${GREEN}$file${NC}..."
        cp "$SOURCE_FILE" "$DEST_FILE"
        
        # Verify copy
        if [ -f "$DEST_FILE" ]; then
            SOURCE_SIZE=$(wc -c < "$SOURCE_FILE")
            DEST_SIZE=$(wc -c < "$DEST_FILE")
            
            if [ "$SOURCE_SIZE" -eq "$DEST_SIZE" ]; then
                echo -e "    ✓ Success (${SOURCE_SIZE} bytes)"
            else
                echo -e "    ${RED}✗ Size mismatch!${NC}"
                exit 1
            fi
        else
            echo -e "    ${RED}✗ Copy failed!${NC}"
            exit 1
        fi
    else
        echo -e "  ${RED}✗ Source file not found: $file${NC}"
        exit 1
    fi
done

echo ""
echo -e "${GREEN}========================================${NC}"
echo -e "${GREEN}Verification${NC}"
echo -e "${GREEN}========================================${NC}"
echo ""

# Verify jQuery version
echo -e "${YELLOW}Verifying jQuery version...${NC}"
JQUERY_VERSION=$(head -n 15 "$DEST_DIR/jquery.js" | grep -o "v[0-9]\+\.[0-9]\+\.[0-9]\+" | head -n 1)

if [ "$JQUERY_VERSION" = "v3.3.1" ]; then
    echo -e "  ✓ jQuery version: ${GREEN}$JQUERY_VERSION${NC}"
else
    echo -e "  ${RED}✗ Version mismatch: Expected v3.3.1, found $JQUERY_VERSION${NC}"
    exit 1
fi

# List copied files
echo ""
echo -e "${YELLOW}Copied files:${NC}"
ls -lh "$DEST_DIR" | tail -n +2 | while read -r line; do
    echo -e "  $line"
done

echo ""
echo -e "${GREEN}========================================${NC}"
echo -e "${GREEN}✓ jQuery Migration Complete!${NC}"
echo -e "${GREEN}========================================${NC}"
echo ""

# Git status
echo -e "${YELLOW}Git Status:${NC}"
echo -e "To commit these changes, run:"
echo -e "${GREEN}  git add new_app/wwwroot/lib/jquery/dist/${NC}"
echo -e "${GREEN}  git commit -m 'Copy jQuery v3.3.1 library files to new_app'${NC}"
echo ""

exit 0
