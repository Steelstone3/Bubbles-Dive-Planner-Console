#[derive(PartialEq, Debug, Copy, Clone)]
pub struct DiveStep {
    pub depth: u32,
    pub time: u32,
}

impl Default for DiveStep {
    fn default() -> Self {
        Self {
            depth: Default::default(),
            time: Default::default(),
        }
    }
}
