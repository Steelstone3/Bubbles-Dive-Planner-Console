use crate::{DiveProfile, DiveStep};
use crate::commands::files::dive_profile::read_dive_profile_file;
use crate::commands::files::dive_step::read_dive_step_file;
use crate::presenters::presenter::read_boolean;

pub fn load_from_default_file() -> (Vec<DiveProfile>, Vec<DiveStep>) {
    let mut dive_steps: Vec<DiveStep> = Vec::new();
    let mut dive_profiles: Vec<DiveProfile> = Vec::new();

    if read_boolean("\nDo you wish to load the current dive plan [y/N]:") {
        dive_steps = read_dive_step_file();
        dive_profiles = read_dive_profile_file();
    }

    (dive_profiles, dive_steps)
}


