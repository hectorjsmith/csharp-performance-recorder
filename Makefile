generateChangelog:
	./tools/git-chglog_linux_amd64 --config tools/chglog/config.yml > CHANGELOG.md

updateVersion:
	./tools/build/updateVersion.sh

build:
	./tools/build/build.sh

docs/downloadTheme:
	wget -O geekdoc.tar.gz https://github.com/thegeeklab/hugo-geekdoc/releases/download/v0.10.1/hugo-geekdoc.tar.gz
	mkdir -p docs/themes/hugo-geekdoc
	tar -xf geekdoc.tar.gz -C docs/themes/hugo-geekdoc/

docs/build:
	cd docs/ && hugo

