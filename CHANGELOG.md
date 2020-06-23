# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Fixed
- Generating a nested result string from a blank result tree returns an empty string instead of throwing an exception (#38)

### Added
- Add changelog file (#35)

## [0.4.0] - 2019-11-19

### Fixed
- Remove the depth data from results when exporting flat results (#30)

### Added
- Support for recording custom methods using lambdas (#28)
- Support exporting result with a custom number of decimal places (#27) 

## [0.3.0] - 2019-11-07

### Fixed
- Fix recorder to ensure method durations are correctly measured if an exception is thrown (#21)

### Added
- Generate NuGet package for library (#17)
- Support for injecting a logger interface to log errors (#20)
- Option to export results up to a maximum depth (#23)
- Option to export results excluding results with a zero duration (#24)


## [0.2.0] - 2019-10-30

### Added
- Support formatting results as a string (#6)
- Generate a recording tree by tracking the depth of nested methods (#8)
- Export recording tree as a nested string (#15)

### Changed
- Refactor the recorder to run before and after each method, instead of around it (#7)
- Improve precision of recordings to allow sub-millisecond durations (#16)

## [0.1.0] - 2019-10-18

- Initial release
