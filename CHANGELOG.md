# CHANGELOG

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog], and this project adheres to [Semantic Versioning].

## [Unreleased]


## [v0.6.0] - 2021-01-03
### Bug Fixes
- (0fe8541) fix: hide namespaces in generic types ([#37](https://gitlab.com/hectorjsmith/csharp-performance-recorder/issues/37))

### Features
- (f0c040b) feat: refactor recording session result interface ([#32](https://gitlab.com/hectorjsmith/csharp-performance-recorder/issues/32))
- (de774c1) feat: split string formatters from result data ([#48](https://gitlab.com/hectorjsmith/csharp-performance-recorder/issues/48))
- (7d4f9ae) feat: auto-generate changelog file based on commits ([#47](https://gitlab.com/hectorjsmith/csharp-performance-recorder/issues/47))

### Merge Requests
- (7a4fa8e) Merge branch '37-show-namespace-flag-should-apply-to-generic-types' into 'develop'
- (7ff92a6) Merge branch '32-support-custom-formatters' into 'develop'
- (f0fb4a4) Merge branch '46-remove-non-deterministic-test' into 'develop'
- (083598d) Merge branch 'agile/update-project-dependencies' into 'develop'
- (7f10d71) Merge branch 'agile/update-aspect-injector' into 'develop'
- (7d8713c) Merge branch '48-split-formatters-from-result-data' into 'develop'
- (d4b5337) Merge branch 'agile/fix-typo-in-property-folder-name' into 'develop'
- (6803fad) Merge branch 'agile/organize-build-tools' into 'develop'
- (dd7d148) Merge branch 'develop' into 'agile/organize-build-tools'
- (89d6846) Merge branch 'agile/update-changelog' into 'develop'
- (ac32caf) Merge branch '47-auto-gen-changelog' into 'develop'

### BREAKING CHANGE

- Remove `Count` property from `IRecordingSessionResult`, use `FlatRecordingData.Count` instead.
- Remove `FlatData()` method from `IRecordingSessionResult`, use the `FlatRecordingData` property instead.
- Remove result formatters from the IRecordingSessionResult interface.
- The CHANGELOG file no longer includes detailed release notes for previous versions.

## [v0.5.0] - 2020-08-05
### Merge Requests
- (f4274de) Merge branch 'release/v0.5.0' into 'develop'
- (b688491) Merge branch '45-prepare-project-to-upload-to-nuget-org' into 'develop'
- (3ff5558) Merge branch '31-label-setter-getter-properties' into 'develop'
- (9921d02) Merge branch '34-update-aspect-injector-library' into 'develop'
- (7e4d3bf) Merge branch '41-update-project-to-treat-all-warnings-as-errors' into 'develop'
- (f840656) Merge branch '44-make-transitive-build-work' into 'develop'
- (22b03a5) Merge branch '33-improve-perf-test' into 'develop'
- (b665cf0) Merge branch '42-setup-issue-and-merge-request-templates' into 'develop'
- (73b21b5) Merge branch 'fix-build-pipepline-badge' into 'develop'
- (187c391) Merge branch '43-job-failed-623178948' into 'develop'
- (cddfe88) Merge branch '38-exception-when-printing-blank-results' into 'master'
- (b107393) Merge branch 'develop' into 'master'
- (f95f569) Merge branch '35-add-changelog-file' into 'develop'
- (3d31740) Merge branch 'develop' into '35-add-changelog-file'
- (2db66b2) Merge branch '36-fix-build-failure-caused-by-incorrect-net-core-version' into 'develop'


## [v0.4.0] - 2019-11-19
### Merge Requests
- (3e52dc6) Merge branch 'release/v0.4.0' into 'master'
- (3ee5f56) Merge branch 'task/25-add-build-script-with-auto-version' into 'develop'
- (29a913b) Merge branch 'task/30-incorrect-depth-data-when-results-are-flattened' into 'develop'
- (ef1ebef) Merge branch 'task/28-support-recording-without-attribute' into 'develop'
- (96e2451) Merge branch 'task/27-custom-result-accuracy' into 'develop'
- (96acedc) Merge branch 'master' into 'develop'


## [v0.3.0] - 2019-11-07
### Merge Requests
- (02c6e10) Merge branch 'release/v0.3.0' into 'master'
- (1b1a546) Merge branch 'task/21-better-exception-handling-in-attribute' into 'develop'
- (609994b) Merge branch 'task/23-support-for-setting-max-depth' into 'develop'
- (403478f) Merge branch 'task/20-allow-injecting-a-logger' into 'develop'
- (74cade7) Merge branch 'task/24-add-option-to-ignore-zero-duration-methods' into 'develop'
- (d0fe47e) Merge branch 'task/17-generate-nuget-package' into 'develop'
- (8479eeb) Merge branch 'release/v0.2.0' into 'develop'


## [v0.2.0] - 2019-10-30
### Merge Requests
- (74ced97) Merge branch 'release/v0.2.0' into 'master'
- (d0c7664) Merge branch 'task/22-duplicates-in-padded-results' into 'develop'
- (7b19cf0) Merge branch 'task/19-record-methods-nested-under-different-methods' into 'develop'
- (465f793) Merge branch 'task/15-export-nested-result-data' into 'develop'
- (2e84fcd) Merge branch 'task/8-record-depth-of-method' into 'develop'
- (2085aa7) Merge branch 'task/7-refactor-recorder-to-run-before-and-after' into 'develop'
- (e07d1dc) Merge branch 'task/6-export-results-as-string-data' into 'develop'
- (4407f0c) Merge branch 'task/16-improve-precision-of-recordings' into 'develop'


## v0.1.0 - 2019-10-18
### Merge Requests
- (00b4f2a) Merge branch 'develop' into 'master'
- (eb1121d) Merge branch 'task/12-update-readme' into 'develop'
- (14dc696) Merge branch 'task/10-code-cleanup' into 'develop'
- (61cd1da) Merge branch 'task/11-split-up-result-object-id' into 'develop'
- (16e940a) Merge branch 'task/3-add-example-project' into 'develop'
- (97135a2) Merge branch 'task/5-allow-retrieving-results' into 'develop'
- (626c53f) Merge branch 'task/2-first-implementation' into 'develop'
- (8fc3a44) Merge branch 'task/1-base-project-setup' into 'develop'

---

*This changelog is automatically generated by [git-chglog]*

[Keep a Changelog]: https://keepachangelog.com/en/1.0.0/
[Semantic Versioning]: https://semver.org/spec/v2.0.0.html
[git-chglog]: https://github.com/git-chglog/git-chglog
[Unreleased]: https://gitlab.com/hectorjsmith/csharp-performance-recorder/compare/v0.6.0...develop
[v0.6.0]: https://gitlab.com/hectorjsmith/csharp-performance-recorder/compare/v0.5.0...v0.6.0
[v0.5.0]: https://gitlab.com/hectorjsmith/csharp-performance-recorder/compare/v0.4.0...v0.5.0
[v0.4.0]: https://gitlab.com/hectorjsmith/csharp-performance-recorder/compare/v0.3.0...v0.4.0
[v0.3.0]: https://gitlab.com/hectorjsmith/csharp-performance-recorder/compare/v0.2.0...v0.3.0
[v0.2.0]: https://gitlab.com/hectorjsmith/csharp-performance-recorder/compare/v0.1.0...v0.2.0
