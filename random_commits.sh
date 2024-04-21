#!/bin/bash

# === Configuration ===
REPO_PATH="/d/Fiverr/TimCoRetailManager"
GITHUB_USER="thatrajeevkr"
EMAIL="thatrajeevkr@gmail.com"
START_DATE="2024-04-01"
END_DATE="2024-07-01"
PUSH_AFTER=true

# Navigate to repo
cd "$REPO_PATH" || { echo "Repo path not found: $REPO_PATH"; exit 1; }

# Configure Git
git config user.name "$GITHUB_USER"
git config user.email "$EMAIL"

# Get all files (excluding .git and .pyc)
readarray -d '' FILES < <(find . -type f ! -path "./.git/*" ! -name "*.pyc" -print0)

echo "Files to commit:"
for f in "${FILES[@]}"; do
  echo "-> $f"
done

# Convert dates to timestamps
start_ts=$(date -d "$START_DATE" +%s)
end_ts=$(date -d "$END_DATE" +%s)

for FILE in "${FILES[@]}"; do
    [[ ! -f "$FILE" ]] && { echo "Skipping non-file: $FILE"; continue; }

    # Generate random timestamp using shuf for full range
    random_ts=$(shuf -i $start_ts-$end_ts -n 1)
    COMMIT_DATE=$(date -d "@$random_ts" "+%Y-%m-%d %H:%M:%S")

    echo "Committing '$FILE' on $COMMIT_DATE"

    git add "$FILE"
    GIT_AUTHOR_DATE="$COMMIT_DATE" GIT_COMMITTER_DATE="$COMMIT_DATE" \
    git commit -m "Add $(basename "$FILE")" || echo "Nothing to commit for '$FILE'"
done

if [ "$PUSH_AFTER" = true ]; then
    echo "Pushing commits to GitHub..."
    git push origin main
fi
